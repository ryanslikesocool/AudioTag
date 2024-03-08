// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag {
	/// <summary>
	/// The runtime object the plays data provided by AudioEffectData.
	/// </summary>
	/// <seealso cref="AudioEffectData"/>
	[RequireComponent(typeof(AudioSource)), DisallowMultipleComponent]
	public class AudioEffect : MonoBehaviour {
		public delegate void OverrideSourceAction(AudioSource source);

		internal AudioEffectData data = null;
		protected int clipIndex = 0;
		public AudioEffectData Data => data;

		public AudioKey Key => data == null ? default : data.key;
		public bool IsActive => gameObject.activeInHierarchy;
		public bool IsPlaying => source != null && source.isPlaying;

		protected AudioSource source = null;
		internal AudioSource Source => source;

		private OverrideSourceAction overrideSource = null;

		// MARK: - Lifecycle

		private void Awake() {
			source = GetComponent<AudioSource>();
		}

		public void Init(AudioEffectData data) {
			this.data = data;
			this.clipIndex = 0;
			gameObject.SetActive(true);
		}

		public void Deinit() {
			this.data = null;
			gameObject.SetActive(false);
		}

		// MARK: -

		/// <summary>
		/// Plays the audio clip with the defined settings.
		/// </summary>
		public AudioEffect Play() {
			if (source == null) {
				Debug.LogError($"Attamped to play an audio effect with no Audio Source.");
			}
			if (data == null) {
				Debug.LogError($"Attamped to play an audio effect with no Audio Effect Data.");
			}
			if (data.clips.Length == 0) {
				Debug.LogError($"Attamped to play audio effect '{Key.key}' with no clips.");
			}

			source.loop = data.loop;
			source.volume = data.volume;
			source.priority = data.priority;

			if (data.clips.Length > 1 && data.randomClip) {
				clipIndex = UnityEngine.Random.Range(0, data.clips.Length);
			}

			if (data.randomPitch) {
				source.pitch = UnityEngine.Random.Range(data.pitchRange.x, data.pitchRange.y);
			} else {
				source.pitch = data.fixedPitch;
			}

			source.spatialBlend = data.spatialBlend;
			source.dopplerLevel = data.dopplerLevel;
			source.spread = data.spread;
			source.minDistance = data.minDistance;
			source.maxDistance = data.maxDistance;

			if (data.mixerGroup != null) {
				source.outputAudioMixerGroup = data.mixerGroup;
			}

			if (!data.isVirtual) {
				source.clip = data.clips[clipIndex];
			}

			overrideSource?.Invoke(source);

			if (data.isVirtual) {
				source.PlayOneShot(data.clips[clipIndex]);
			} else {
				source.Play();
			}

			overrideSource = default;

			return this;
		}

		/// <summary>
		/// Overrides the clip index of the AudioEffect.
		/// This setting will be in place until the next call to Play().
		/// </summary>
		/// <param name="clipIndex">The index of the clip to play.</param>
		public AudioEffect SetClipIndex(int clipIndex) {
			int index;
			if (clipIndex < 0 || clipIndex >= data.clips.Length) {
				index = 0;
				Debug.LogWarning($"The desired clip index ({clipIndex}) is out of range 0 ..< {data.clips.Length} on AudioEffect with name '{gameObject.name}' with key '{Key}'.  The clip index will be set to 0.");
			} else {
				index = clipIndex;
			}
			overrideSource += (source) => {
				source.clip = data.clips[index];
			};
			return this;
		}

		/// <summary>
		/// Overrides the volume of the AudioEffect, clamped between 0 and 1.
		/// This setting will be in place until the next call to Play().
		/// </summary>
		/// <param name="value">The volume of the AudioEffect.</param>
		public AudioEffect SetVolume(float value) {
			overrideSource += (source) => {
				source.volume = Mathf.Clamp01(value);
			};
			return this;
		}

		/// <summary>
		/// Overrides the pitch of the AudioEffect, clamped between -3 and 3.
		/// This setting will be in place until the next call to Play().
		/// </summary>
		/// <param name="pitch">The pitch of the AudioEffect.</param>
		public AudioEffect SetPitch(float pitch) {
			overrideSource += (source) => {
				source.pitch = Mathf.Clamp(pitch, -3, 3);
			};
			return this;
		}

		/// <summary>
		/// Sets the world position of the AuidoEffect.
		/// Useful for spatial audio.
		/// </summary>
		/// <param name="position">The world position of the AudioEffect.</param>
		public AudioEffect SetPosition(Vector3 position) {
			transform.position = position;
			return this;
		}

		/// <summary>
		/// Openly modify the AudioSource attached to this AudioEffect.
		/// Any settings applied will be in place until the next call to Play().
		/// </summary>
		public AudioEffect OverrideSource(OverrideSourceAction action) {
			overrideSource += action;
			return this;
		}

		/// <summary>
		/// Stops the AudioEffect.
		/// This is only necessary for stopping an effect early or stopping a looping effect.
		/// </summary>
		public AudioEffect Stop() {
			source.Stop();
			return this;
		}

		public AudioEffect Load() {
			data.Load();
			return this;
		}

		public AudioEffect Unload() {
			data.Unload();
			return this;
		}
	}
}