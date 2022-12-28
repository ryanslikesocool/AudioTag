// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using Foundation;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
    [DisallowMultipleComponent]
    public sealed class AudioPool : Singleton<AudioPool> {
        [SerializeField] private AudioMixer mixer = null;
#if ODIN_INSPECTOR_3
        [SerializeField, Searchable] private AudioEffectSet[] sets = new AudioEffectSet[0];
        [SerializeField, Searchable] private AudioEffectData[] data = new AudioEffectData[0];
        [SerializeField, Tooltip("The prefab to use by default when creating audio sources.  This cannot be empty.  Assign this to the prefab included in the package folder if a custom one is not needed.")] private AudioEffect sourcePrefab = null;
        [SerializeField, Tooltip("Mark instantiated AudioEffect objects with this hide flag.")] private HideFlags effectHideFlags = HideFlags.HideAndDontSave;
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<int, AudioEffectSet> setLink = null;
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<int, AudioEffect> prefabLink = null;
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<int, List<AudioEffect>> effectLink = null;
#else
        [SerializeField] private AudioEffectSet[] sets = new AudioEffectSet[0];
        [SerializeField] private AudioEffectData[] data = new AudioEffectData[0];
        [SerializeField, Tooltip("The prefab to use by default when creating audio sources.  This cannot be empty.  Assign this to the prefab included in the package folder if a custom one is not needed.")] private AudioEffect sourcePrefab = null;
        [SerializeField, Tooltip("Mark instantiated AudioEffect objects with this hide flag.")] private HideFlags effectHideFlags = HideFlags.HideAndDontSave;
        private Dictionary<int, AudioEffectSet> setLink = null;
        private Dictionary<int, AudioEffect> prefabLink = null;
        private Dictionary<int, List<AudioEffect>> effectLink = null;
#endif

        private AudioEffectData[] AllData => sets.FlatMap(s => s.data);

        protected override void Awake() {
            base.Awake();
            Strings.Clear();

            foreach (AudioEffectSet set in sets) {
                if (set.loadOnLaunch) {
                    set.Load();
                }
            }
        }

        private void OnEnable() {
            setLink = new Dictionary<int, AudioEffectSet>();
            prefabLink = new Dictionary<int, AudioEffect>();
            effectLink = new Dictionary<int, List<AudioEffect>>();

            foreach (AudioEffectData d in AllData) {
                int id = d.ID;
                if (prefabLink.ContainsKey(id)) {
                    Debug.LogError($"The audio ID ('{id}', possibly named '{Strings.Get(id)}') assigned to {d.name} already exists.  Please retag one of these objects.  {d.name} will not be added to the link.");
                    return;
                }

                AudioEffect e = CreatePrefab(d);

                prefabLink.Add(id, e);
                effectLink.Add(id, new List<AudioEffect>());
            }

            foreach (AudioEffectSet set in sets) {
                setLink.Add(set.ID, set);
            }
        }

        private AudioEffect CreatePrefab(in AudioEffectData data) {
            AudioEffect prefab = data.prefabOverride == null ? sourcePrefab : data.prefabOverride;
            AudioEffect e = Instantiate(prefab);
            e.gameObject.hideFlags = effectHideFlags;

            e.Init(data);

            return e;
        }

        private AudioEffect GetInstance(in int id) {
            if (Shared.effectLink.TryGetValue(id, out List<AudioEffect> effects)) {
                if (effects.Count > 0) {
                    return effects.First(e => e.Active && (!e.Playing || e.IsVirtual));
                }

                AudioEffect result = Instantiate(Shared.prefabLink[id]);
                result.gameObject.hideFlags = Shared.effectHideFlags;

                result.Init(Shared.prefabLink[id].data);
                effects.Add(result);
                return result;
            }

            Debug.LogWarning($"AudioEffect with ID '{id}' (with possible tag '{Strings.Get(id)}') does not exist.");
            return null;
        }

        /// <summary>
        /// Peek the next available AudioEffect with the defined tag.
        /// </summary>
        /// <param name="tag">The tag to look for.</param>
        /// <returns>The AudioEffect with the defined tag, if one was found.</returns>
        public static AudioEffect Peek(in string tag) => Peek(Strings.Add(tag));

        /// <summary>
        /// Peek the next available AudioEffect with the defined ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The AudioEffect with the defined ID, if one was found.</returns>
        public static AudioEffect Peek(in int id) {
            if (id == 0) {
                Debug.LogError("The effect ID cannot be 0.");
                return null;
            }

            return Shared.GetInstance(id);
        }

        public static void LoadSet(in AudioEffectSet set) => LoadSet(set.ID);

        public static AudioEffectSet LoadSet(in string tag) => LoadSet(Strings.Add(tag));

        public static AudioEffectSet LoadSet(in int id) {
            if (id == 0) {
                Debug.LogError("The set ID cannot be 0.");
                return null;
            }

            if (Shared.setLink.TryGetValue(id, out AudioEffectSet set)) {
                set.Load();
                return set;
            } else {
                Debug.LogWarning($"AudioEffectSet with ID '{id}' (with possible tag '{Strings.Get(id)}') does not exist.");
                return null;
            }
        }

        public static void UnloadSet(in AudioEffectSet set) => UnloadSet(set.ID);

        public static void UnloadSet(in string tag) => UnloadSet(Strings.Add(tag));

        public static void UnloadSet(in int id) {
            if (id == 0) {
                Debug.LogError("The set ID cannot be 0.");
                return;
            }

            if (Shared.setLink.TryGetValue(id, out AudioEffectSet set)) {
                set.Unload();
            } else {
                Debug.LogWarning($"AudioEffectSet with ID '{id}' (with possible tag '{Strings.Get(id)}') does not exist.");
            }
        }

        /// <summary>
        /// Play the next available AudioEffect with the defined tag.
        /// </summary>
        /// <param name="tag">The tag to look for.</param>
        /// <returns>The AudioEffect with the defined tag, if one was found.</returns>
        public static AudioEffect Play(in string tag) => Play(Strings.Add(tag));

        /// <summary>
        /// Play the next available AudioEffect with the defined ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The AudioEffect with the defined ID, if one was found.</returns>
        public static AudioEffect Play(int id) => Peek(id).Play();

        public static void SetMixerVolume(in string tag, in float percent) {
            if (percent == 0) {
                Shared.mixer.SetFloat(tag, -80);
            } else {
                Shared.mixer.SetFloat(tag, 20f * Mathf.Log10(percent));
            }
        }

        public static void AddSet(in AudioEffectSet set) {
            Shared.setLink.Add(set.ID, set);
            foreach (AudioEffectData data in set.data) {
                Shared.AddEffect(data);
            }
        }

        public static void RemoveSet(in AudioEffectSet set) {
            foreach (AudioEffectData data in set.data) {
                Shared.RemoveEffect(data);
            }
            Shared.setLink.Remove(set.ID);
        }

        private void AddEffect(in AudioEffectData data) {
            int id = data.ID;
            if (prefabLink.ContainsKey(id)) {
                Debug.LogError($"The audio ID ('{id}', possibly named '{Strings.Get(id)}') assigned to {data.name} already exists.  Please retag one of these objects.  {data.name} will not be added to the link.");
                return;
            }

            AudioEffect prefab = data.prefabOverride ?? this.sourcePrefab;

            prefabLink.Add(id, prefab);
            effectLink.Add(id, new List<AudioEffect>());
        }

        private void RemoveEffect(in AudioEffectData data) {
            int id = data.ID;
            if (!effectLink.TryGetValue(id, out List<AudioEffect> effects)) { return; }

            foreach (AudioEffect e in effects) {
                e.Deinit();
                GameObject.Destroy(e.gameObject);
            }
            effects.Clear();
            effectLink.Remove(id);
            prefabLink.Remove(id);
        }
    }
}