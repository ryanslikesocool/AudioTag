// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Spatial/Spatialize Post Effects",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 6 + 1
	)]
	public sealed class SpatializePostEffects : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.SpatializePostEffects(value);
	}
}
