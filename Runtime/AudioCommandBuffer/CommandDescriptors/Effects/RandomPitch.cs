// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Effects/Pitch (Random)",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 4 + 1
	)]
	public sealed class RandomPitch : AudioCommandDescriptor {
		public ClosedRange<float> value = new ClosedRange<float>(0.75f, 1.25f);

		// MARK: -

		public override IAudioCommand Resolve()
			=> AudioCommand.Pitch.Random(value);
	}
}
