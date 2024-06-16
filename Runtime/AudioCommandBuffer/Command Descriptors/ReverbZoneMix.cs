namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class ReverbZoneMix : AudioCommandDescriptor {
				public float value = 1;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.ReverbZoneMix(value);
			}
		}
	}
}