// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Playback/Priority",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 10 + 1
	)]
	public sealed class Priority : AudioCommandDescriptor {
		[Range(0, 256)] public int value = 128;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Priority(value);
	}
}
