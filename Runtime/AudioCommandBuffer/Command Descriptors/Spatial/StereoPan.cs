// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Spatial/Stereo Pan",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 8 + 1
	)]
	public sealed class StereoPan : AudioCommandDescriptor {
		[Range(-1.0f, 1.0f)] public float value = 0;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.StereoPan(value);
	}
}
