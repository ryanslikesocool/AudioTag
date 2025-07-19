// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Playback/Clip (Constant)",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 12
	)]
	public sealed class Clip : AudioCommandDescriptor {
		public AudioClip value = null;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Clip(value);
	}
}
