using Foundation;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {

			public sealed class RandomVolume : AudioCommandDescriptor {
				public ClosedRange<float> value = new ClosedRange<float>(0.5f, 1.0f);

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.Volume(value);
			}
		}
	}
}