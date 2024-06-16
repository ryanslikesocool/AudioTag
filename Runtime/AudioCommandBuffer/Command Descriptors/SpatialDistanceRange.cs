using Foundation;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class SpatialDistanceRange : AudioCommandDescriptor {
				public ClosedRange<float> value = new ClosedRange<float>(1, 500);

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.SpatialDistanceRange(value);
			}
		}
	}
}