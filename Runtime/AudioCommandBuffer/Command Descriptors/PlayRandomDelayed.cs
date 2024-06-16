using Foundation;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class PlayRandomDelayed : AudioCommandDescriptor {
				public ClosedRange<float> value = new ClosedRange<float>(0.75f, 1.25f);

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.Play(value);
			}
		}
	}
}