using UnityEngine.Audio;

namespace AudioTag {
	public sealed class MixerAudioCommandDescriptor : AudioCommandDescriptor {
		public AudioMixerGroup value = null;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new MixerAudioCommand(value);
	}
}