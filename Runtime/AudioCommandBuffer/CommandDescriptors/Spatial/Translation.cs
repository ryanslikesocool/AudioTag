// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Spatial/Translation (Constant)",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 12
	)]
	public sealed class Translation : AudioCommandDescriptor {
		public Space space = Space.Self;
		public Vector3 value = Vector3.zero;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Translation(space, value);
	}
}
