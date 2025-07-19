// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Effects/Volume (Random)",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 6 + 3
	)]
	public sealed class RandomVolume : AudioCommandDescriptor {
		public ClosedRange<float> value = new ClosedRange<float>(0.5f, 1.0f);

		// MARK: -

		public override IAudioCommand Resolve()
			=> AudioCommand.Volume.Random(value);
	}
}
