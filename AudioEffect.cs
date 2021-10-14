using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEffect : MonoBehaviour
    {
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

        public AudioEffect Init()
        {
            ID = tag.GetTagID();
            return this;
        }

        public AudioEffect Play()
        {
            if (clips.Length > 1 && randomClip)
            {
                clipIndex = UnityEngine.Random.Range(0, clips.Length);
            }
            if (randomPitch)
            {
                source.pitch = UnityEngine.Random.Range(pitchRange.x, pitchRange.y);
            }

            if (isVirtual)
            {
                source.PlayOneShot(clips[clipIndex]);
            }
            else
            {
                source.clip = clips[clipIndex];
                source.Play();
            }

            return this;
        }

        public AudioEffect SetClipIndex(int value)
        {
            clipIndex = value;
            return this;
        }

        public AudioEffect SetVolume(float value)
        {
            source.volume = value;
            return this;
        }

        public AudioEffect SetPitch(float value)
        {
            source.pitch = value;
            return this;
        }
    }
}