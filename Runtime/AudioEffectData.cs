// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Foundation;

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/AudioTag/Audio Effect Data")]
	public class AudioEffectData : ScriptableObject {
		public AudioKey key = default;

		public AudioEffect prefabOverride = null;
		public AudioMixerGroup mixerGroup = null;
		public bool isVirtual = true;
		public bool loop = false;
		[Range(0, 1)] public float volume = 1;
		[Range(0, 256)] public int priority = 128;

		public AudioClip[] clips = new AudioClip[0];
		public bool randomClip = false;
		public int clipIndex = 0;

		public bool randomPitch = false;
		[Range(-3, 3)] public float fixedPitch = 1;
		public Vector2 pitchRange = Vector2.one;

		[Range(0, 1)] public float spatialBlend = 0;
		[Range(0, 1.1f)] public float reverbZoneMix = 1;
		[Range(0, 5)] public float dopplerLevel = 1;
		[Range(0, 360)] public float spread = 0;
		public float minDistance = 1;
		public float maxDistance = 500;

		public bool RequiresLoading => clips.Any(clip => clip != null ? !clip.preloadAudioData : false);
		public LoadState LoadState => clips.Reduce(LoadState.None, (result, element) => {
			if (element != null) {
				return result | element.loadState.ToAudioTag();
			} else {
				return result;
			}
		});

		public float MaxDuration => clips.Max(clip => clip.length);

		// MARK: -

		public float GetPitch()
			=> randomPitch ? Random.Range(pitchRange.x, pitchRange.y) : fixedPitch;

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