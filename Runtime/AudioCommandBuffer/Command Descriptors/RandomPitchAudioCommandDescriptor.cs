using Foundation;

namespace AudioTag {
	public sealed class RandomPitchAudioCommandDescriptor : AudioCommandDescriptor {
		public ClosedRange<float> value = new ClosedRange<float>(0.75f, 1.25f);

		// MARK: -

		public override IAudioCommand Resolve()
			=> new PitchAudioCommand(value);
	}
}