namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class Loop : AudioCommandDescriptor {
				public bool value = false;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.Loop(value);
			}
		}
	}
}