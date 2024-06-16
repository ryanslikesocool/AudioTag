namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class Play : AudioCommandDescriptor {
				public override IAudioCommand Resolve()
					=> new AudioCommand.Play();
			}
		}
	}
}