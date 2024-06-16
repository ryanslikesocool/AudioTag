// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;
using UnityEngine;

namespace AudioTag {
	/// <summary>
	/// The runtime object the plays data provided by <see cref="AudioDescriptor"/>.
	/// </summary>
	/// <seealso cref="AudioDescriptor"/>
	[RequireComponent(typeof(AudioSource)), DisallowMultipleComponent]
	public partial class AudioInstance : MonoBehaviour {
		public delegate void OverrideSourceAction(AudioSource source);

		[Get] internal AudioDescriptor descriptor = null;

		public bool IsActive => gameObject.activeInHierarchy;
		public bool IsPlaying => source != null && source.isPlaying;

		[Get, GetComponent] protected AudioSource source = null;

		private OverrideSourceAction overrideSource = null;

		// MARK: - Lifecycle

		private void Awake() {
			InitializeComponents();
		}

		public void Init(AudioDescriptor descriptor) {
			this.descriptor = descriptor;
			gameObject.SetActive(true);
		}

		public void Deinit() {
			this.descriptor = null;
			gameObject.SetActive(false);
		}

		// MARK: -

		/// <summary>
		/// Plays the audio clip with the defined settings.
		/// </summary>
		public AudioInstance Play() {
			Guard.Throw.NotNull(source);
			Guard.Throw.NotNull(descriptor);
			Guard.Throw.Precondition(!descriptor.Clips.IsEmpty);

			source.loop = descriptor.loop;
			source.volume = descriptor.volume;
			source.priority = descriptor.priority;

			if (descriptor.clips.Length > 1 && descriptor.randomClip) {
				clipIndex = UnityEngine.Random.Range(0, descriptor.clips.Length);
			}

			if (descriptor.randomPitch) {
				source.pitch = UnityEngine.Random.Range(descriptor.pitchRange.x, descriptor.pitchRange.y);
			} else {
				source.pitch = descriptor.fixedPitch;
			}

			source.spatialBlend = descriptor.spatialBlend;
			source.dopplerLevel = descriptor.dopplerLevel;
			source.spread = descriptor.spread;
			source.minDistance = descriptor.minDistance;
			source.maxDistance = descriptor.maxDistance;

			if (descriptor.mixerGroup != null) {
				source.outputAudioMixerGroup = descriptor.mixerGroup;
			}

			if (!descriptor.isVirtual) {
				source.clip = descriptor.clips[clipIndex];
			}

			overrideSource?.Invoke(source);

			if (descriptor.isVirtual) {
				source.PlayOneShot(descriptor.clips[clipIndex]);
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
		public AudioInstance SetClipIndex(int clipIndex) {
			int index;
			if (clipIndex < 0 || clipIndex >= descriptor.clips.Length) {
				index = 0;
				Debug.LogWarning($"The desired clip index ({clipIndex}) is out of the range [0 ..< {descriptor.clips.Length}] on AudioEffect with name '{gameObject.name}' with data {descriptor.name}.  The clip index will be set to 0.", descriptor);
			} else {
				index = clipIndex;
			}
			overrideSource += (source) => {
				source.clip = descriptor.clips[index];
			};
			return this;
		}

		/// <summary>
		/// Overrides the volume of the AudioEffect, clamped between 0 and 1.
		/// This setting will be in place until the next call to Play().
		/// </summary>
		/// <param name="value">The volume of the AudioEffect.</param>
		public AudioInstance SetVolume(float value) {
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
		public AudioInstance SetPitch(float pitch) {
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
		public AudioInstance SetPosition(Vector3 position) {
			transform.position = position;
			return this;
		}

		/// <summary>
		/// Openly modify the AudioSource attached to this AudioEffect.
		/// Any settings applied will be in place until the next call to Play().
		/// </summary>
		public AudioInstance OverrideSource(OverrideSourceAction action) {
			overrideSource += action;
			return this;
		}

		/// <summary>
		/// Stops the AudioEffect.
		/// This is only necessary for stopping an effect early or stopping a looping effect.
		/// </summary>
		public AudioInstance Stop() {
			source.Stop();
			return this;
		}

		public AudioInstance Load() {
			descriptor.Load();
			return this;
		}

		public AudioInstance Unload() {
			descriptor.Unload();
			return this;
		}
	}
}