using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/AudioTag/Audio Effect Data")]
	public sealed class AudioEffectData : ScriptableObject {
#if ODIN_INSPECTOR_3
		[BoxGroup("Info"), Tooltip("The effect's key, used to access the audio effect in code.")] public AudioKey key = new AudioKey();

		[BoxGroup("General"), Tooltip("Override the default audio source prefab with this.  Leave empty for the default, or if you're unsure.")] public AudioEffect prefabOverride = null;
		[BoxGroup("General"), Tooltip("The Audio Mixer Group to output to.  Setting this will override the value in a containing AudioEffectSet")] public AudioMixerGroup mixerGroup = null;
		[BoxGroup("General"), Tooltip("Should the audio source be marked virtual?")] public bool isVirtual = true;
		[BoxGroup("General")] public bool loop = false;
		[BoxGroup("General"), Range(0, 1)] public float volume = 1;
		[BoxGroup("General"), Range(0, 256)] public int priority = 128;

		[BoxGroup("Clip"), ListDrawerSettings(DefaultExpandedState = true)] public AudioClip[] clips = new AudioClip[0];
		[BoxGroup("Clip"), DisableIf("$randomClip"), HideIf("@clips.Length < 2"), PropertyRange(0, "@clips.Length - 1")] public int clipIndex = 0;
		[BoxGroup("Clip"), HideIf("@clips.Length < 2")] public bool randomClip = false;

		[BoxGroup("Pitch")] public bool randomPitch = false;
		[BoxGroup("Pitch"), Range(-3, 3), HideIf("$randomPitch")] public float fixedPitch = 1;
		[BoxGroup("Pitch"), ShowIf("$randomPitch")] public Vector2 pitchRange = Vector2.one;

		[BoxGroup("Spatial"), Range(0, 1)] public float spatialBlend = 0;
		[BoxGroup("Spatial"), EnableIf("@(spatialBlend > 0)"), Range(0, 1.1f)] public float reverbZoneMix = 1;
		[BoxGroup("Spatial"), EnableIf("@(spatialBlend > 0)"), Range(0, 5)] public float dopplerLevel = 1;
		[BoxGroup("Spatial"), EnableIf("@(spatialBlend > 0)"), Range(0, 360)] public float spread = 0;
		[BoxGroup("Spatial"), EnableIf("@(spatialBlend > 0)")] public float minDistance = 1;
		[BoxGroup("Spatial"), EnableIf("@(spatialBlend > 0)")] public float maxDistance = 500;

		[BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool RequiresLoading => clips.Any(clip => clip != null ? !clip.preloadAudioData : false);
		[BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsLoaded => clips.All(clip => clip != null ? clip.loadState == AudioDataLoadState.Loaded : true);
		[BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsLoading => clips.Any(clip => clip != null ? clip.loadState == AudioDataLoadState.Loading : true);
		[BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsUnloaded => clips.Any(clip => clip != null ? clip.loadState == AudioDataLoadState.Unloaded : true);
#else
        [Tooltip("The effect's key, used to access the audio effect in code.")] public AudioKey key = new AudioKey();

        [Header("General"), Tooltip("Override the default audio source prefab with this.  Leave empty for the default, or if you're unsure.")] public AudioEffect prefabOverride = null;
        [Tooltip("The Audio Mixer Group to output to.  Setting this will override the value in a containing AudioEffectSet")] public AudioMixerGroup mixerGroup = null;
        [Tooltip("Should the audio source be marked virtual?")] public bool isVirtual = true;
        public bool loop = false;
        [Range(0, 1)] public float volume = 1;
        [Range(0, 256)] public int priority = 128;

        [Header("Clip")] public AudioClip[] clips = new AudioClip[0];
        public int clipIndex = 0;
        public bool randomClip = false;

        [Header("Pitch")] public bool randomPitch = false;
        [Range(-3, 3)] public float fixedPitch = 1;
        public Vector2 pitchRange = Vector2.one;

        [Header("Spatial"), Range(0, 1)] public float spatialBlend = 0;
        [Range(0, 1.1f)] public float reverbZoneMix = 1;
        [Range(0, 5)] public float dopplerLevel = 1;
        [Range(0, 360)] public float spread = 0;
        public float minDistance = 1;
        public float maxDistance = 500;

        public bool RequiresLoading => clips.Any(clip => clip != null ? !clip.preloadAudioData : false);
        public bool IsLoaded => clips.All(clip => clip != null ? clip.loadState == AudioDataLoadState.Loaded : true);
        public bool IsLoading => clips.Any(clip => clip != null ? clip.loadState == AudioDataLoadState.Loading : true);
        public bool IsUnloaded => clips.Any(clip => clip != null ? clip.loadState == AudioDataLoadState.Unloaded : true);
#endif

		public float Duration => clips.Max(clip => clip.length);

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