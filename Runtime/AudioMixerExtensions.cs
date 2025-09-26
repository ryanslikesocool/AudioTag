using UnityEngine;
using UnityEngine.Audio;

namespace AudioTag {
	public static partial class AudioMixerExtensions {
		public static bool SetVolumePercent(this AudioMixer mixer, string name, float percent) {
			percent = Mathf.Clamp01(percent);

			if (percent == 0) {
				return mixer.SetFloat(name, -80);
			} else {
				return mixer.SetFloat(name, 20f * Mathf.Log10(percent));
			}
		}
	}
}
