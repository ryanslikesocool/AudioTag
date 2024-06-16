// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class SpatialDistanceRange : AudioCommandDescriptor {
		public ClosedRange<float> value = new ClosedRange<float>(1, 500);

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.SpatialDistanceRange(value);
	}
}
