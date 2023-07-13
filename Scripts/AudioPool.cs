// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System.Collections.Generic;
using Foundation;
using UnityEngine;
using UnityEngine.Audio;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif
using UnityEngine.Pool;
using ClockKit;

namespace AudioTag {
    [DisallowMultipleComponent]
    public sealed class AudioPool : Singleton<AudioPool> {
        [SerializeField] private AudioMixer mixer = null;
#if ODIN_INSPECTOR_3
        [SerializeField, BoxGroup("Data"), Searchable] private AudioEffectSet[] sets = default;
        [SerializeField, BoxGroup("Data"), Searchable] private AudioEffectData[] data = default;
        [SerializeField, BoxGroup("Object"), Tooltip("The prefab to use by default when creating audio sources.  This cannot be empty.  Assign this to the prefab included in the package folder if a custom one is not needed.")] private AudioEffect sourcePrefab = default;
        [SerializeField, BoxGroup("Object"), Tooltip("Mark instantiated AudioEffect objects with this hide flag.")] private HideFlags effectHideFlags = HideFlags.HideAndDontSave;
        [SerializeField, BoxGroup("Pooling")] private bool collectionChecks = true;
        [SerializeField, BoxGroup("Pooling")] private int defaultCapacity = 10;
        [SerializeField, BoxGroup("Pooling")] private int maxSize = 10_000;
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<AudioEffectTag.Runtime, AudioEffectSet> setLink = default;
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<AudioEffectTag.Runtime, AudioEffect> prefabLink = default;
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<AudioEffectTag.Runtime, List<AudioEffect>> effectLink = default;
#else
        [SerializeField, Title("Data")] private AudioEffectSet[] sets = default;
        [SerializeField] private AudioEffectData[] data = default;
        [SerializeField, Title("Object"), Tooltip("The prefab to use by default when creating audio sources.  This cannot be empty.  Assign this to the prefab included in the package folder if a custom one is not needed.")] private AudioEffect sourcePrefab = default;
        [SerializeField, Tooltip("Mark instantiated AudioEffect objects with this hide flag.")] private HideFlags effectHideFlags = HideFlags.HideAndDontSave;
        [SerializeField, Title("Pooling")] private bool collectionChecks = true;
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxSize = 10_000;
        private Dictionary<int, AudioEffectSet> setLink = default;
        private Dictionary<int, AudioEffect> prefabLink = default;
        private Dictionary<int, List<AudioEffect>> effectLink = default;
#endif
        private ObjectPool<AudioEffect> effectPool = default;

        private IEnumerable<AudioEffectData> AllData => Foundation.Extensions.Join(sets.FlatMap(s => s.data), data);

        protected override void Awake() {
            base.Awake();

            foreach (AudioEffectSet set in sets) {
                if (set.loadOnLaunch) {
                    set.Load();
                }
            }

            Init();
        }

        private void Init() {
            setLink = new Dictionary<AudioEffectTag.Runtime, AudioEffectSet>();
            prefabLink = new Dictionary<AudioEffectTag.Runtime, AudioEffect>();
            effectLink = new Dictionary<AudioEffectTag.Runtime, List<AudioEffect>>();

            foreach (AudioEffectData d in AllData) {
                AudioEffectTag.Runtime tag = d.tag;
                if (prefabLink.ContainsKey(tag)) {
                    Debug.LogError($"The audio tag assigned to {d.name} already exists.  Please retag one of these objects.  {d.name} will not be added to the link.");
                    return;
                }

                AudioEffect e = CreatePrefab(d);

                prefabLink.Add(tag, e);
                effectLink.Add(tag, new List<AudioEffect>());
            }

            foreach (AudioEffectSet set in sets) {
                setLink.Add(set.tag, set);
            }

            effectPool = new ObjectPool<AudioEffect>(
                createFunc: PoolCreate,
                actionOnGet: PoolGet,
                actionOnRelease: PoolRelease,
                actionOnDestroy: PoolDestroy,
                collectionCheck: collectionChecks,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
            );
        }

        private AudioEffect CreatePrefab(in AudioEffectData data) {
            AudioEffect prefab = data.prefabOverride == null ? sourcePrefab : data.prefabOverride;
            AudioEffect e = Instantiate(prefab);
            e.gameObject.hideFlags = effectHideFlags;

            e.Init(data);

            return e;
        }

        private AudioEffect GetInstance(in AudioEffectTag.Runtime tag) {
            if (Shared.effectLink.TryGetValue(tag, out List<AudioEffect> effects)) {
                if (effects.Count > 0) {
                    return Foundation.Extensions.First(effects, e => e.Active && (!e.Playing || e.IsVirtual));
                }

                AudioEffect result = Instantiate(Shared.prefabLink[tag]);
                result.gameObject.hideFlags = Shared.effectHideFlags;

                result.Init(Shared.prefabLink[tag].data);
                effects.Add(result);
                return result;
            }

            //Debug.LogWarning($"AudioEffect with ID '{id}' (with possible tag '{Strings.Get(id)}') does not exist.");
            return null;
        }

        /// <summary>
        /// Peek the next available AudioEffect with the defined tag.
        /// </summary>
        /// <param name="tag">The tag to look for.</param>
        /// <returns>The AudioEffect with the defined tag, if one was found.</returns>
        public static AudioEffect Peek(in AudioEffectTag.Runtime tag) => Shared.GetInstance(tag);

        public static void LoadSet(in AudioEffectSet set) => LoadSet(set.tag);

        public static AudioEffectSet LoadSet(in AudioEffectTag.Runtime tag) {
            if (Shared.setLink.TryGetValue(tag, out AudioEffectSet set)) {
                set.Load();
                return set;
            } else {
                Debug.LogWarning($"AudioEffectSet with tag '{tag}' does not exist.");
                return null;
            }
        }

        public static void UnloadSet(in AudioEffectSet set) => UnloadSet(set.tag);

        public static void UnloadSet(in AudioEffectTag.Runtime tag) {
            if (Shared.setLink.TryGetValue(tag, out AudioEffectSet set)) {
                set.Unload();
            } else {
                Debug.LogWarning($"AudioEffectSet with tag '{tag}' does not exist.");
            }
        }

        /// <summary>
        /// Play the next available AudioEffect with the defined tag.
        /// </summary>
        /// <param name="tagid">The tag to look for.</param>
        /// <returns>The AudioEffect with the defined tag, if one was found.</returns>
        public static AudioEffect Play(in AudioEffectTag.Runtime tag) => Peek(tag)?.Play();

        public static void SetMixerVolume(in string tag, in float percent) {
            if (percent == 0) {
                Shared.mixer.SetFloat(tag, -80);
            } else {
                Shared.mixer.SetFloat(tag, 20f * Mathf.Log10(percent));
            }
        }

        public static void AddSet(in AudioEffectSet set) {
            Shared.setLink.Add(set.tag, set);
            foreach (AudioEffectData data in set.data) {
                Shared.AddEffect(data);
            }
        }

        public static void RemoveSet(in AudioEffectSet set) {
            foreach (AudioEffectData data in set.data) {
                Shared.RemoveEffect(data);
            }
            Shared.setLink.Remove(set.tag);
        }

        private void AddEffect(in AudioEffectData data) {
            AudioEffectTag.Runtime tag = data.tag;
            if (prefabLink.ContainsKey(tag)) {
                Debug.LogError($"The audio tag assigned to {data.name} already exists.  Please retag one of these objects.  {data.name} will not be added to the link.");
                return;
            }

            AudioEffect prefab = data.prefabOverride ?? this.sourcePrefab;

            prefabLink.Add(tag, prefab);
            effectLink.Add(tag, new List<AudioEffect>());
        }

        private void RemoveEffect(in AudioEffectData data) {
            AudioEffectTag.Runtime tag = data.tag;
            if (!effectLink.TryGetValue(tag, out List<AudioEffect> effects)) { return; }

            foreach (AudioEffect e in effects) {
                e.Deinit();
                GameObject.Destroy(e.gameObject);
            }
            effects.Clear();
            effectLink.Remove(tag);
            prefabLink.Remove(tag);
        }

        // MARK: - Pooling

        private AudioEffect PoolCreate() {
            AudioEffect result = Instantiate(sourcePrefab);
            result.gameObject.hideFlags = effectHideFlags;
            return result;
        }

        private void PoolGet(AudioEffect effect) {
            // TODO
        }

        private void PoolRelease(AudioEffect effect) {
            // TODO
            effect.Deinit();
        }

        private void PoolDestroy(AudioEffect effect) {
            Destroy(effect.gameObject);
        }

        public static AudioEffect Peek(in AudioEffectData data) {
            if (data == null) {
                return null;
            }

            AudioEffect result = Shared.effectPool.Get();
            if (data != null) {
                result.Init(data);
            }
            return result;
        }

        public static AudioEffect Play(in AudioEffectData data, bool autoReturn = true) {
            AudioEffect result = Peek(data)?.Play();

            if (autoReturn && result?.ActiveClip != null) {
                CKClock.Delay(seconds: result.ActiveClip.length * result.ActivePitch, () => Return(result));
            }
            return result;
        }

        public static void Unload(in AudioEffect effect, bool returnToPool = true) {
            effect.Unload();
            if (returnToPool) {
                Return(effect);
            }
        }

        public static void Return(in AudioEffect effect) => Shared.effectPool.Release(effect);
    }
}