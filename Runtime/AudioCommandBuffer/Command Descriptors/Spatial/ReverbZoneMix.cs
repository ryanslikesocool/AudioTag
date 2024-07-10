// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Spatial/Reverb Zone Mix",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 10
	)]
	public sealed class ReverbZoneMix : AudioCommandDescriptor {
		public float value = 1;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.ReverbZoneMix(value);
	}
}
