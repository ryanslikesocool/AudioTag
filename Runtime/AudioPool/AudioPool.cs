// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using Foundation;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using ClockKit;
using System.Linq;

namespace AudioTag {
	/// <summary>
	/// The primary object managing all AudioEffects.
	/// </summary>
	/// <seealso cref="AudioInstance"/>
	[DisallowMultipleComponent, SingletonComponent(persistent = true)]
	public sealed partial class AudioPool : MonoBehaviour {
		public AudioMixer mixer = null;

		[SerializeField] private AudioEffectSet[] sets = default;
		[SerializeField] private AudioDescriptor[] data = default;

		[SerializeField, Tooltip("The prefab to use by default when creating audio sources.  This cannot be empty.  Assign this to the prefab included in the package folder if a custom one is not needed.")] private AudioInstance sourcePrefab = default;
		[SerializeField, Tooltip("Mark instantiated AudioEffect objects with this hide flag.")] private HideFlags effectHideFlags = HideFlags.HideAndDontSave;

		private HashSet<AudioEffectSet> setLink = default;
		private Dictionary<AudioInstance, List<AudioInstance>> prefabPool = default; // { key: prefab, value: [instance] }
		private ObjectPool<AudioInstance> effectPool = default;

		private IEnumerable<AudioDescriptor> AllData => sets.FlatMap(s => s.data).Join(data);

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
			setLink = new HashSet<AudioEffectSet>(sets);
			prefabPool = new Dictionary<AudioInstance, List<AudioInstance>>();

			foreach (AudioDescriptor data in AllData) {
				if (prefabLink.ContainsKey(data)) {
					Debug.LogWarning($"The audio key assigned to {data.name} already exists.  {data.name} will not be added to the link.");
					continue;
				}

				AudioInstance effect = CreatePrefab(data);

				prefabLink.Add(data, effect);
				effectLink.Add(data, new List<AudioInstance>());
			}

			effectPool = new ObjectPool<AudioInstance>(
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

		private AudioInstance CreatePrefab(in AudioDescriptor data) {
			Guard.Throw.NotNull(data);

			AudioInstance prefab = data.prefabOverride == null ? sourcePrefab : data.prefabOverride;
			AudioInstance effect = Instantiate(prefab);
			effect.gameObject.hideFlags = effectHideFlags;

			effect.Init(data);

			return effect;
		}

		private AudioInstance GetInstance(in AudioDescriptor key) {
			if (Shared.effectLink.TryGetValue(key, out List<AudioInstance> effects)) {
				if (effects.Count > 0) {
					return effects.First(e => e.IsActive && (!e.IsPlaying || e.descriptor.isVirtual));
				}

				AudioInstance result = Instantiate(Shared.prefabLink[key]);
				result.gameObject.hideFlags = Shared.effectHideFlags;

				result.Init(Shared.prefabLink[key].data);
				effects.Add(result);
				return result;
			}

			//Debug.LogWarning($"AudioEffect with ID '{id}' (with possible key '{Strings.Get(id)}') does not exist.");
			return null;
		}

		/// <summary>
		/// Attempt to peek the next available <see cref="AudioInstance"/> with the given data.
		/// </summary>
		/// <param name="data">The data to look for.</param>
		/// <param name="effect">The <see cref="AudioInstance"/> with the given <paramref name="data"/>, if one was found.</param>
		/// <returns><see langword="true"/> if the <paramref name="effect"/> was found; <see langword="false"/> otherwise.</returns>
		public static bool TryPeek(in AudioDescriptor data, out AudioInstance effect) {
			effect = Peek(data);
			return effect != null;
		}

		/// <summary>
		/// Play the next available <see cref="AudioInstance"/> with the given key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns>The <see cref="AudioInstance"/> with the given <paramref name="key"/>, if one was found.</returns>
		public static AudioInstance Play(in AudioDescriptor key) {
			if (TryPlay(key, out AudioInstance effect)) {
				return effect;
			} else {
				return null;
			}
		}

		/// <summary>
		/// Play the next available <see cref="AudioInstance"/> with the given key.
		/// </summary>
		/// <param name="key">The key to look for.</param>
		/// <returns>The <see cref="AudioInstance"/> with the given <paramref name="key"/>, if one was found.</returns>
		/// <returns><see langword="true"/> if the <paramref name="effect"/> was found; <see langword="false"/> otherwise.</returns>
		public static bool TryPlay(in AudioDescriptor data, out AudioInstance effect) {
			if (TryPeek(data, out effect)) {
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

		private AudioInstance PoolCreate() {
			AudioInstance result = Instantiate(sourcePrefab);
			result.gameObject.hideFlags = effectHideFlags;
			return result;
		}

		private void PoolGet(AudioInstance effect) {
			// TODO
		}

		private void PoolRelease(AudioInstance effect) {
			// TODO
			effect.Deinit();
		}

		private void PoolDestroy(AudioInstance effect) {
			Destroy(effect.gameObject);
		}

		/// <summary>
		/// Peek the next available <see cref="AudioInstance"/> with the given data.
		/// </summary>
		/// <param name="data">The data to look for.</param>
		/// <returns>An <see cref="AudioInstance"/> with the given <paramref name="data"/>, if one was found.</returns>
		public static AudioInstance Peek(in AudioDescriptor data) {
			Guard.Throw.NotNull(data);

			AudioInstance result;
			if (data.prefabOverride == null) {
				result = Shared.effectPool.Get();
			} else {
				result = Shared.effectLink[];
			}
			result.Init(data);

			return result;
		}

		public static AudioInstance Play(in AudioDescriptor data, bool autoReturn = true)
			=> Play(Peek(data), autoReturn);

		public static AudioInstance Play(in AudioInstance effect, bool autoReturn = true) {
			Guard.Throw.NotNull(effect);

			AudioInstance result = effect.Play();

			if (autoReturn && result.Source.clip != null) {
				CKClock.Delay(seconds: result.Source.clip.length * result.Source.pitch, () => Return(result));
			}
			return result;
		}

		public static void Return(in AudioInstance effect) => Shared.effectPool.Release(effect);
	}
}