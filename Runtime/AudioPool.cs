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
		[BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<AudioKey, AudioEffectSet> setLink = default;
		[BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<AudioKey, AudioEffect> prefabLink = default;
		[BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<AudioKey, List<AudioEffect>> effectLink = default;
#else
        [SerializeField, Title("Data")] private AudioEffectSet[] sets = default;
        [SerializeField] private AudioEffectData[] data = default;
        [SerializeField, Title("Object"), Tooltip("The prefab to use by default when creating audio sources.  This cannot be empty.  Assign this to the prefab included in the package folder if a custom one is not needed.")] private AudioEffect sourcePrefab = default;
        [SerializeField, Tooltip("Mark instantiated AudioEffect objects with this hide flag.")] private HideFlags effectHideFlags = HideFlags.HideAndDontSave;
        [SerializeField, Title("Pooling")] private bool collectionChecks = true;
        [SerializeField] private int defaultCapacity = 10;
        [SerializeField] private int maxSize = 10_000;
        private Dictionary<AudioKey, AudioEffectSet> setLink = default;
        private Dictionary<AudioKey, AudioEffect> prefabLink = default;
        private Dictionary<AudioKey, List<AudioEffect>> effectLink = default;
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
			setLink = new Dictionary<AudioKey, AudioEffectSet>();
			prefabLink = new Dictionary<AudioKey, AudioEffect>();
			effectLink = new Dictionary<AudioKey, List<AudioEffect>>();

			foreach (AudioEffectData d in AllData) {
				AudioKey key = d.key;
				if (prefabLink.ContainsKey(key)) {
					Debug.LogWarning($"The audio key assigned to {d.name} already exists.  {d.name} will not be added to the link.");
					return;
				}

				AudioEffect e = CreatePrefab(d);

				prefabLink.Add(key, e);
				effectLink.Add(key, new List<AudioEffect>());
			}

			foreach (AudioEffectSet set in sets) {
				setLink.Add(set.key, set);
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

		private AudioEffect GetInstance(in AudioKey key) {
			if (Shared.effectLink.TryGetValue(key, out List<AudioEffect> effects)) {
				if (effects.Count > 0) {
					return Foundation.Extensions.First(effects, e => e.Active && (!e.Playing || e.IsVirtual));
				}

				AudioEffect result = Instantiate(Shared.prefabLink[key]);
				result.gameObject.hideFlags = Shared.effectHideFlags;

				result.Init(Shared.prefabLink[key].data);
				effects.Add(result);
				return result;
			}

			//Debug.LogWarning($"AudioEffect with ID '{id}' (with possible key '{Strings.Get(id)}') does not exist.");
			return null;
		}

		/// <summary>
		/// Peek the next available AudioEffect with the defined key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns>The AudioEffect with the defined key, if one was found.</returns>
		public static AudioEffect Peek(in AudioKey key) => Shared.GetInstance(key);

		public static void LoadSet(in AudioEffectSet set) => LoadSet(set.key);

		public static AudioEffectSet LoadSet(in AudioKey key) {
			if (Shared.setLink.TryGetValue(key, out AudioEffectSet set)) {
				set.Load();
				return set;
			} else {
				Debug.LogWarning($"AudioEffectSet with key '{key.key}' does not exist.");
				return null;
			}
		}

		public static void UnloadSet(in AudioEffectSet set) => UnloadSet(set.key);

		public static void UnloadSet(in AudioKey key) {
			if (Shared.setLink.TryGetValue(key, out AudioEffectSet set)) {
				set.Unload();
			} else {
				Debug.LogWarning($"AudioEffectSet with key '{key.key}' does not exist.");
			}
		}

		/// <summary>
		/// Play the next available AudioEffect with the defined key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns>The AudioEffect with the defined key, if one was found.</returns>
		public static AudioEffect Play(in AudioKey key) => Peek(key)?.Play();

		public static void SetMixerVolume(in string name, in float percent) {
			if (percent == 0) {
				Shared.mixer.SetFloat(name, -80);
			} else {
				Shared.mixer.SetFloat(name, 20f * Mathf.Log10(percent));
			}
		}

		public static void AddSet(in AudioEffectSet set) {
			Shared.setLink.Add(set.key, set);
			foreach (AudioEffectData data in set.data) {
				Shared.AddEffect(data);
			}
		}

		public static void RemoveSet(in AudioEffectSet set) {
			foreach (AudioEffectData data in set.data) {
				Shared.RemoveEffect(data);
			}
			Shared.setLink.Remove(set.key);
		}

		private void AddEffect(in AudioEffectData data) {
			AudioKey key = data.key;
			if (prefabLink.ContainsKey(key)) {
				Debug.LogError($"The audio key assigned to {data.name} already exists.  Please rekey one of these objects.  {data.name} will not be added to the link.");
				return;
			}

			AudioEffect prefab = data.prefabOverride ?? this.sourcePrefab;

			prefabLink.Add(key, prefab);
			effectLink.Add(key, new List<AudioEffect>());
		}

		private void RemoveEffect(in AudioEffectData data) {
			AudioKey key = data.key;
			if (!effectLink.TryGetValue(key, out List<AudioEffect> effects)) { return; }

			foreach (AudioEffect e in effects) {
				e.Deinit();
				GameObject.Destroy(e.gameObject);
			}
			effects.Clear();
			effectLink.Remove(key);
			prefabLink.Remove(key);
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