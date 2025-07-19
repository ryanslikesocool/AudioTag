// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine.Audio;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Playback/Mixer Group",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 10
	)]
	public sealed class MixerGroup : AudioCommandDescriptor {
		public AudioMixerGroup value = null;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.MixerGroup(value);
	}
}
