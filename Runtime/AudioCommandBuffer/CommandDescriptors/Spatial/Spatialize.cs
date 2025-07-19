// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Spatial/Spatialize",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 6
	)]
	public sealed class Spatialize : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Spatialize(value);
	}
}
