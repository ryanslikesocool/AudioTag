// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Effects/Volume (Constant)",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 6 + 2
	)]
	public sealed class Volume : AudioCommandDescriptor {
		[Range(0.0f, 1.0f)] public float value = 1;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Volume(value);
	}
}