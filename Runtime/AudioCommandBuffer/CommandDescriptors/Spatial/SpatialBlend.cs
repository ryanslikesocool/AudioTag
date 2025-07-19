// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Spatial/Blend",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 8
	)]
	public sealed class SpatialBlend : AudioCommandDescriptor {
		[Range(0.0f, 1.0f)] public float value = 0;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.SpatialBlend(value);
	}
}
