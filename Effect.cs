// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
    [RequireComponent(typeof(AudioSource))]
    public class Effect : MonoBehaviour {
#if ODIN_INSPECTOR_3
        [BoxGroup("Info"), SerializeField] private new string tag = string.Empty;
        [BoxGroup("Info"), ShowInInspector, ReadOnly] public int ID { get; protected set; }
        [HorizontalGroup("Info/H1"), ToggleLeft, ShowInInspector, ReadOnly] public bool Active => gameObject.activeInHierarchy;
        [HorizontalGroup("Info/H1"), ToggleLeft, ShowInInspector, ReadOnly] public bool Playing => source == null ? false : source.isPlaying;

        [BoxGroup("Audio"), SerializeField] protected AudioSource source = null;
        [BoxGroup("Audio")] public bool isVirtual = true;
        [BoxGroup("Audio/Clip"), SerializeField] protected AudioClip[] clips = new AudioClip[0];
        [BoxGroup("Audio/Clip"), SerializeField, HideIf("$randomClip"), PropertyRange(0, "@clips.Length - 1")] protected int clipIndex = 0;
        [BoxGroup("Audio/Clip"), SerializeField] protected bool randomClip = false;
        [BoxGroup("Audio/Pitch"), SerializeField] protected bool randomPitch = false;
        [BoxGroup("Audio/Pitch"), SerializeField, ShowIf("$randomPitch")] protected Vector2 pitchRange = Vector2.one;
#else
        [Header("Info"), SerializeField] private new string tag = string.Empty;
        public int ID { get; protected set; }
        public bool Active => gameObject.activeInHierarchy;
        public bool Playing => source == null ? false : source.isPlaying;

        [Header("Audio"), SerializeField] protected AudioSource source = null;
        public bool isVirtual = true;
        [SerializeField] protected AudioClip[] clips = new AudioClip[0];
        [SerializeField] protected int clipIndex = 0;
        [SerializeField] protected bool randomClip = false;
        [SerializeField] protected bool randomPitch = false;
        [SerializeField] protected Vector2 pitchRange = Vector2.one;
#endif

        internal Effect Init() {
            ID = tag.GetTagID();
            return this;
        }

        /// <summary>
        /// Plays the audio clip with the defined settings.
        /// </summary>
        public Effect Play() {
            if (clips.Length > 1 && randomClip) {
                clipIndex = UnityEngine.Random.Range(0, clips.Length);
            }
            if (randomPitch) {
                source.pitch = UnityEngine.Random.Range(pitchRange.x, pitchRange.y);
            }

            if (isVirtual) {
                source.PlayOneShot(clips[clipIndex]);
            } else {
                source.clip = clips[clipIndex];
                source.Play();
            }

            return this;
        }

        /// <summary>
        /// Sets the clip index of the Effect.
        /// </summary>
        /// <param name="value">The index of the clip to play.</param>
        public Effect SetClipIndex(int value) {
            if (value < 0 || value >= clips.Length) {
                clipIndex = 0;
                Debug.LogWarning($"The desired clip index ({value}) is out of range 0 ..< {clips.Length} on Effect with name \"{gameObject.name}\" and tag \"{tag}\".  The clip index will be set to 0.");
            } else {
                clipIndex = value;
            }
            return this;
        }

        /// <summary>
        /// Sets the volume of the Effect, clamped between 0 and 1.
        /// </summary>
        /// <param name="value">The volume of the Effect.</param>
        public Effect SetVolume(float value) {
            source.volume = Mathf.Clamp01(value);
            return this;
        }

        /// <summary>
        /// Sets the pitch of the Effect, clamped between -3 and 3.
        /// </summary>
        /// <param name="value">The pitch of the Effect.</param>
        public Effect SetPitch(float value) {
            source.pitch = Mathf.Clamp(value, -3, 3);
            return this;
        }
    }
}