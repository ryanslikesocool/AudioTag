// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Effects/Pitch (Constant)",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 4
	)]
	public sealed class Pitch : AudioCommandDescriptor {
		[Range(-3, 3)] public float value = 1;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Pitch(value);
	}
}
