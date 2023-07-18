// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
	[RequireComponent(typeof(AudioSource))]
	public class AudioEffect : MonoBehaviour {
		internal AudioEffectData data = null;
		protected int clipIndex = 0;
		public AudioEffectData Data => data;

#if ODIN_INSPECTOR_3
		[BoxGroup("Info"), ShowInInspector, ReadOnly] public AudioKey.Runtime key => data?.key ?? AudioKey.Runtime.zero;
		[HorizontalGroup("Info/H1"), ToggleLeft, ShowInInspector, ReadOnly] public bool Active => gameObject.activeInHierarchy;
		[HorizontalGroup("Info/H1"), ToggleLeft, ShowInInspector, ReadOnly] public bool Playing => source == null ? false : source.isPlaying;
#else
        public AudioKey.Runtime key => data.key;
        public bool Active => gameObject.activeInHierarchy;
        public bool Playing => source == null ? false : source.isPlaying;
#endif
		public AudioClip ActiveClip => source.clip;
		public float ActivePitch => source.pitch;

		[SerializeField] protected AudioSource source = null;

		public bool IsVirtual => data.isVirtual;
		protected bool Loop => data.loop;
		protected AudioClip[] Clips => data.clips;
		protected bool RandomClip => data.randomClip;
		protected bool RandomPitch => data.randomPitch;
		protected Vector2 PitchRange => data.pitchRange;
		protected float FixedPitch => data.fixedPitch;
		protected float SpatialBlend => data.spatialBlend;
		protected float ReverbZoneMix => data.reverbZoneMix;
		protected float DopplerLevel => data.dopplerLevel;
		protected float Spread => data.spread;
		protected float MinDistance => data.minDistance;
		protected float MaxDistance => data.maxDistance;

		private bool overrideAny = false;
		private bool overrideClipIndex = false;
		private bool overrideVolume = false;
		private bool overridePitch = false;
		private bool override3D = false;

		public void Init(AudioEffectData data) {
			this.data = data;
			this.clipIndex = 0;
			gameObject.SetActive(true);
		}

		public void Deinit() {
			this.data = null;
			gameObject.SetActive(false);
		}

		/// <summary>
		/// Plays the audio clip with the defined settings.
		/// </summary>
		public AudioEffect Play() {
			if (!overrideAny) {
				source.loop = Loop;

				if (!overrideClipIndex) {
					if (Clips.Length > 1 && RandomClip) {
						clipIndex = UnityEngine.Random.Range(0, Clips.Length);
					}
				}
				if (!overridePitch) {
					if (RandomPitch) {
						source.pitch = UnityEngine.Random.Range(PitchRange.x, PitchRange.y);
					} else {
						source.pitch = FixedPitch;
					}
				}
				if (!overrideVolume) {
					source.volume = data.volume;
				}
				if (!override3D) {
					source.spatialBlend = SpatialBlend;
					source.dopplerLevel = DopplerLevel;
					source.spread = Spread;
					source.minDistance = MinDistance;
					source.maxDistance = MaxDistance;
				}
			}

			if (data.mixerGroup != null) {
				source.outputAudioMixerGroup = data.mixerGroup;
			}

			if (IsVirtual) {
				source.PlayOneShot(Clips[clipIndex]);
			} else {
				source.clip = Clips[clipIndex];
				source.Play();
			}

			overrideAny = false;
			overrideClipIndex = false;
			overrideVolume = false;
			overridePitch = false;
			override3D = false;

			return this;
		}

		/// <summary>
		/// Overrides the clip index of the AudioEffect.
		/// This setting will be in place until the next call to Play().
		/// </summary>
		/// <param name="value">The index of the clip to play.</param>
		public AudioEffect SetClipIndex(int value) {
			if (value < 0 || value >= Clips.Length) {
				clipIndex = 0;
				Debug.LogWarning($"The desired clip index ({value}) is out of range 0 ..< {Clips.Length} on AudioEffect with name '{gameObject.name}' with key '{key}'.  The clip index will be set to 0.");
			} else {
				clipIndex = value;
			}
			overrideClipIndex = true;
			return this;
		}

		/// <summary>
		/// Overrides the volume of the AudioEffect, clamped between 0 and 1.
		/// This setting will be in place until the next call to Play().
		/// </summary>
		/// <param name="value">The volume of the AudioEffect.</param>
		public AudioEffect SetVolume(float value) {
			source.volume = Mathf.Clamp01(value);
			overrideVolume = true;
			return this;
		}

		/// <summary>
		/// Overrides the pitch of the AudioEffect, clamped between -3 and 3.
		/// This setting will be in place until the next call to Play().
		/// </summary>
		/// <param name="value">The pitch of the AudioEffect.</param>
		public AudioEffect SetPitch(float value) {
			source.pitch = Mathf.Clamp(value, -3, 3);
			overrideVolume = false;
			return this;
		}

		/// <summary>
		/// Sets the world position of the AuidoEffect.
		/// Useful for spatial audio.
		/// </summary>
		/// <param name="value">The world position of the AudioEffect.</param>
		public AudioEffect SetPosition(Vector3 position) {
			transform.position = position;
			return this;
		}

		/// <summary>
		/// Openly modify the AudioSource attached to this AudioEffect.
		/// Any settings applied will be in place until the next call to Play().
		/// </summary>
		/// <param name="value">The pitch of the AudioEffect.</param>
		public AudioEffect ModifySource(Action<AudioSource> action) {
			action(source);
			overrideAny = true;
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