// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System.Collections.Generic;
using Foundation;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using ClockKit;
using Sirenix.OdinInspector;

namespace AudioTag {
	/// <summary>
	/// The primary object managing all AudioEffects.
	/// </summary>
	/// <seealso cref="AudioEffect"/>
	[DisallowMultipleComponent, Singleton(Persistent = true)]
	public sealed partial class AudioPool : MonoBehaviour {
		public AudioMixer mixer = null;

		[Title("Data")]
		[SerializeField] private AudioEffectSet[] sets = default;
		[SerializeField] private AudioEffectData[] data = default;

		[Title("Object")]
		[SerializeField, Tooltip("The prefab to use by default when creating audio sources.  This cannot be empty.  Assign this to the prefab included in the package folder if a custom one is not needed.")] private AudioEffect sourcePrefab = default;
		[SerializeField, Tooltip("Mark instantiated AudioEffect objects with this hide flag.")] private HideFlags effectHideFlags = HideFlags.HideAndDontSave;

		[Title("Pool")]
		[SerializeField] private bool collectionChecks = true;
		[SerializeField, Min(0)] private int defaultCapacity = 10;
		[SerializeField, Min(1)] private int maxSize = 10_000;

		[ShowInInspector, ReadOnly] private Dictionary<string, AudioEffectSet> setLink = default;
		[ShowInInspector, ReadOnly] private Dictionary<string, AudioEffect> prefabLink = default;
		[ShowInInspector, ReadOnly] private Dictionary<string, List<AudioEffect>> effectLink = default;
		private ObjectPool<AudioEffect> effectPool = default;

		private IEnumerable<AudioEffectData> AllData => Extensions.Join(sets.FlatMap(s => s.data), data);

		// MARK: - Lifecycle

		private void Awake() {
			foreach (AudioEffectSet set in sets) {
				if (set.loadOnLaunch) {
					set.Load();
				}
			}

			Init();
		}

		private void Init() {
			setLink = new Dictionary<string, AudioEffectSet>();
			prefabLink = new Dictionary<string, AudioEffect>();
			effectLink = new Dictionary<string, List<AudioEffect>>();

			foreach (AudioEffectSet set in sets) {
				setLink.Add(set.key, set);
			}

			foreach (AudioEffectData d in AllData) {
				AudioKey key = d.key;
				if (prefabLink.ContainsKey(key)) {
					Debug.LogWarning($"The audio key assigned to {d.name} already exists.  {d.name} will not be added to the link.");
					continue;
				}

				AudioEffect e = CreatePrefab(d);

				prefabLink.Add(key, e);
				effectLink.Add(key, new List<AudioEffect>());
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

		private void OnDestroy() {
			DeinitializeSingleton();
		}

		// MARK: -

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
					return Extensions.First(effects, e => e.Active && (!e.Playing || e.IsVirtual));
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
		/// Peek the next available <see cref="AudioEffect"/> with the given key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns>The <see c=> with the given <paramref name="key"/>, if one was found.</returns>
		public static AudioEffect Peek(in AudioKey key)
			=> Shared.GetInstance(key);

		/// <summary>
		/// </summary>

		/// <summary>
		/// Attempt to peek the next available <see cref="AudioEffect"/> with the given key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <param name="effect">The <see cref="AudioEffect"/> with the given <paramref name="key"/>, if one was found.</param>
		/// <returns><see langword="true"/> if the <paramref name="effect"/> was found; <see langword="false"/> otherwise.</returns>
		public static bool TryPeek(in AudioKey key, out AudioEffect effect) {
			effect = Peek(key);
			return effect != null;
		}

		/// <summary>
		/// Play the next available <see cref="AudioEffect"/> with the given key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns>The <see cref="AudioEffect"/> with the given <paramref name="key"/>, if one was found.</returns>
		public static AudioEffect Play(in AudioKey key) {
			if (TryPlay(key, out AudioEffect effect)) {
				return effect;
			} else {
				return null;
			}
		}

		/// <summary>
		/// Play the next available <see cref="AudioEffect"/> with the given key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns>The <see cref="AudioEffect"/> with the given <paramref name="key"/>, if one was found.</returns>
		/// <returns><see langword="true"/> if the <paramref name="effect"/> was found; <see langword="false"/> otherwise.</returns>
		public static bool TryPlay(in AudioKey key, out AudioEffect effect) {
			if (TryPeek(key, out effect)) {
				effect.Play();
				return true;
			} else {
				return false;
			}
		}

		public static void SetMixerVolume(in string name, in float percent) {
			if (percent == 0) {
				Shared.mixer.SetFloat(name, -80);
			} else {
				Shared.mixer.SetFloat(name, 20f * Mathf.Log10(percent));
			}
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

		public static AudioEffect Play(in AudioEffectData data, bool autoReturn = true)
			=> Play(Peek(data), autoReturn);

		public static AudioEffect Play(in AudioEffect effect, bool autoReturn = true) {
			AudioEffect result = effect?.Play();

			if (autoReturn && result?.ActiveClip != null) {
				CKClock.Delay(seconds: result.ActiveClip.length * result.ActivePitch, () => Return(result));
			}
			return result;
		}

		public static void Return(in AudioEffect effect) => Shared.effectPool.Release(effect);
	}
}