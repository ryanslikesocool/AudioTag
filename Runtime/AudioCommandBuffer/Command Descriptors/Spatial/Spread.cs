// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Spatial/Spread",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 8 + 3
	)]
	public sealed class Spread : AudioCommandDescriptor {
		[Range(0.0f, 360.0f)] public float value = 0;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Spread(value);
	}
}
