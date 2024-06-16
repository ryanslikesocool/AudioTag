namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class BypassReverbZones : AudioCommandDescriptor {
				public bool value = false;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.BypassReverbZones(value);
			}
		}
	}
}