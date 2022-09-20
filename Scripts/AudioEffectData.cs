using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
    [CreateAssetMenu(menuName = "Developed With Love/AudioTag/Audio Effect")]
    public class AudioEffectData : ScriptableObject {
#if ODIN_INSPECTOR_3
        [BoxGroup("Info"), Tooltip("The effect's tag, used to access the audio effect in code.")] public string tag = string.Empty;
        [BoxGroup("Info"), Tooltip("The internal ID used for runtime access.  Do not reference this value directly, as it may change."), ShowInInspector, ReadOnly] public int ID => Strings.Add(tag);

        [BoxGroup("General"), Tooltip("Override the default audio source prefab with this.  Leave empty for the default, or if you're unsure.")] public AudioEffect prefabOverride = null;
        [BoxGroup("General"), Tooltip("The Audio Mixer Group to output to.  Setting this will override the value in a containing AudioEffectSet")] public AudioMixerGroup mixerGroup = null;
        [BoxGroup("General"), Tooltip("Should the audio source be marked virtual?")] public bool isVirtual = true;
        [BoxGroup("General"), Range(0, 1)] public float volume = 1;

        [BoxGroup("Clip"), ListDrawerSettings(Expanded = true)] public AudioClip[] clips = new AudioClip[0];
        [BoxGroup("Clip"), DisableIf("$randomClip"), HideIf("@clips.Length < 2"), PropertyRange(0, "@clips.Length - 1")] public int clipIndex = 0;
        [BoxGroup("Clip"), HideIf("@clips.Length < 2")] public bool randomClip = false;
        [BoxGroup("Pitch")] public bool randomPitch = false;
        [BoxGroup("Pitch"), Range(-3, 3), HideIf("$randomPitch")] public float fixedPitch = 1;
        [BoxGroup("Pitch"), ShowIf("$randomPitch")] public Vector2 pitchRange = Vector2.one;

        [BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool RequiresLoading => clips.Any(clip => !clip.preloadAudioData);
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsLoaded => clips.All(clip => clip.loadState == AudioDataLoadState.Loaded);
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsLoading => clips.Any(clip => clip.loadState == AudioDataLoadState.Loading);
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsUnloaded => clips.Any(clip => clip.loadState == AudioDataLoadState.Unloaded);
#else
        [Tooltip("The effect's tag, used to access the audio effect in code.")] public string tag = string.Empty;
        [Tooltip("The internal ID used for runtime access.  Do not reference this value directly, as it may change.")] public int ID => Strings.Add(tag);

        [Header("General"), Tooltip("Override the default audio source prefab with this.  Leave empty for the default, or if you're unsure.")] public AudioEffect prefabOverride = null;
        [Tooltip("The Audio Mixer Group to output to.  Setting this will override the value in a containing AudioEffectSet")] public AudioMixerGroup mixerGroup = null;
        [Tooltip("Should the audio source be marked virtual?")] public bool isVirtual = true;
        [Range(0, 1)] public float volume = 1;

        [Header("Clip")] public AudioClip[] clips = new AudioClip[0];
        public int clipIndex = 0;
        public bool randomClip = false;
        [Header("Pitch")] public bool randomPitch = false;
        [Range(-3, 3)] public float fixedPitch = 1;
        public Vector2 pitchRange = Vector2.one;

        public bool RequiresLoading => clips.Any(clip => !clip.preloadAudioData);
        public bool IsLoaded => clips.All(clip => clip.loadState == AudioDataLoadState.Loaded);
        public bool IsLoading => clips.Any(clip => clip.loadState == AudioDataLoadState.Loading);
        public bool IsUnloaded => clips.Any(clip => clip.loadState == AudioDataLoadState.Unloaded);
#endif

        public void Load() {
            foreach (AudioClip clip in clips) {
                if (clip.loadState == AudioDataLoadState.Unloaded) {
                    clip.LoadAudioData();
                }
            }
        }

        public void Unload() {
            foreach (AudioClip clip in clips) {
                if (clip.loadState != AudioDataLoadState.Unloaded) {
                    clip.UnloadAudioData();
                }
            }
        }
    }
}