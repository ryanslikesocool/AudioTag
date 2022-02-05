using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
    [CreateAssetMenu(menuName = "Developed With Love/AudioTag/Audio Effect")]
    public class AudioEffectData : ScriptableObject {
#if ODIN_INSPECTOR_3
        [BoxGroup("Info"), Tooltip("The effect's tag, used to access the audio effect in code.")] public string tag = string.Empty;
        [BoxGroup("Info"), Tooltip("The internal ID used for runtime access.  Do not reference this value directly."), ShowInInspector, ReadOnly] public int ID => Strings.Add(tag);

        [BoxGroup("General"), Tooltip("Override the default audio source prefab with this.  Leave empty for the default, or if you're unsure.")] public AudioEffect prefabOverride = null;
        [BoxGroup("General"), Tooltip("Should the audio source be marked virtual?")] public bool isVirtual = true;
        [BoxGroup("Clip")] public AudioClip[] clips = new AudioClip[0];
        [BoxGroup("Clip"), DisableIf("$randomClip"), HideIf("@clips.Length < 2"), PropertyRange(0, "@clips.Length - 1")] public int clipIndex = 0;
        [BoxGroup("Clip"), HideIf("@clips.Length < 2")] public bool randomClip = false;
        [BoxGroup("Pitch")] public bool randomPitch = false;
        [BoxGroup("Pitch"), ShowIf("$randomPitch")] public Vector2 pitchRange = Vector2.one;
#else
        [Tooltip("The effect's tag, used to access the audio effect in code.")] public string tag = string.Empty;
        public int ID => Strings.Add(tag);

        [Header("General"), Tooltip("Override the default audio source prefab with this.  Leave empty for the default, or if you're unsure.")] public AudioEffect prefabOverride = null;
        [Tooltip("Should the audio source be marked virtual?")] public bool isVirtual = true;
        [Header("Clip")] public AudioClip[] clips = new AudioClip[0];
        [DisableIf("$randomClip"), HideIf("@clips.Length < 2"), PropertyRange(0, "@clips.Length - 1")] public int clipIndex = 0;
        public bool randomClip = false;
        [Header("Pitch")] public bool randomPitch = false;
        public Vector2 pitchRange = Vector2.one;
#endif
    }
}