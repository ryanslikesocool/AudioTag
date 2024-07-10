// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Playback/Clip (Random)",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 12 + 1
	)]
	public sealed class RandomClip : AudioCommandDescriptor {
		public AudioClip[] value = new AudioClip[0];

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Clip(value);
	}
}
