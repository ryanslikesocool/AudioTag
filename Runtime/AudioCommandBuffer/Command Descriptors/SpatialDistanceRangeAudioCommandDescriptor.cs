using Foundation;

namespace AudioTag {
	public sealed class SpatialDistanceRangeAudioCommandDescriptor : AudioCommandDescriptor {
		public ClosedRange<float> value = new ClosedRange<float>(1, 500);

		// MARK: -

		public override IAudioCommand Resolve()
			=> new SpatialDistanceRangeAudioCommand(value);
	}
}