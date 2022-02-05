// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
    [RequireComponent(typeof(AudioSource))]
    public class AudioEffect : MonoBehaviour {
        internal AudioEffectData data = null;
        protected int clipIndex = 0;
        protected string Tag => data.tag;

#if ODIN_INSPECTOR_3
        [BoxGroup("Info"), ShowInInspector, ReadOnly] public int ID => data == null ? 0 : data.ID;
        [HorizontalGroup("Info/H1"), ToggleLeft, ShowInInspector, ReadOnly] public bool Active => gameObject.activeInHierarchy;
        [HorizontalGroup("Info/H1"), ToggleLeft, ShowInInspector, ReadOnly] public bool Playing => source == null ? false : source.isPlaying;

        [SerializeField] protected AudioSource source = null;
        public bool IsVirtual => data.isVirtual;
        protected AudioClip[] Clips => data.clips;
        protected bool RandomClip => data.randomClip;
        protected bool RandomPitch => data.randomPitch;
        protected Vector2 PitchRange => data.pitchRange;
#else
        public int ID => data.ID;
        public bool Active => gameObject.activeInHierarchy;
        public bool Playing => source == null ? false : source.isPlaying;

        [SerializeField] protected AudioSource source = null;
        public bool IsVirtual => data.isVirtual;
        protected AudioClip[] Clips => data.clips;
        protected bool RandomClip = false;
        protected bool RandomPitch = false;
        protected Vector2 PitchRange => data.pitchRange;
#endif

        internal void Prepare(AudioEffectData data) {
            this.data = data;
            this.clipIndex = 0;
        }

        /// <summary>
        /// Plays the audio clip with the defined settings.
        /// </summary>
        public AudioEffect Play() {
            if (Clips.Length > 1 && RandomClip) {
                clipIndex = UnityEngine.Random.Range(0, Clips.Length);
            }
            if (RandomPitch) {
                source.pitch = UnityEngine.Random.Range(PitchRange.x, PitchRange.y);
            }

            if (IsVirtual) {
                source.PlayOneShot(Clips[clipIndex]);
            } else {
                source.clip = Clips[clipIndex];
                source.Play();
            }

            return this;
        }

        /// <summary>
        /// Sets the clip index of the AudioEffect.
        /// </summary>
        /// <param name="value">The index of the clip to play.</param>
        public AudioEffect SetClipIndex(int value) {
            if (value < 0 || value >= Clips.Length) {
                clipIndex = 0;
                Debug.LogWarning($"The desired clip index ({value}) is out of range 0 ..< {Clips.Length} on AudioEffect with name '{gameObject.name}' with tag '{Tag}'.  The clip index will be set to 0.");
            } else {
                clipIndex = value;
            }
            return this;
        }

        /// <summary>
        /// Sets the volume of the AudioEffect, clamped between 0 and 1.
        /// </summary>
        /// <param name="value">The volume of the AudioEffect.</param>
        public AudioEffect SetVolume(float value) {
            source.volume = Mathf.Clamp01(value);
            return this;
        }

        /// <summary>
        /// Sets the pitch of the AudioEffect, clamped between -3 and 3.
        /// </summary>
        /// <param name="value">The pitch of the AudioEffect.</param>
        public AudioEffect SetPitch(float value) {
            source.pitch = Mathf.Clamp(value, -3, 3);
            return this;
        }
    }
}