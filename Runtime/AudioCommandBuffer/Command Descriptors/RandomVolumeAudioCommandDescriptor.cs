using Foundation;

namespace AudioTag {
	public sealed class RandomVolumeAudioCommandDescriptor : AudioCommandDescriptor {
		public ClosedRange<float> value = new ClosedRange<float>(0.5f, 1.0f);

		// MARK: -

		public override IAudioCommand Resolve()
			=> new VolumeAudioCommand(value);
	}
}