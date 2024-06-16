// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class DelayCommands : AudioCommandDescriptor {
		public float duration = 1.0f;
		public AudioCommandDescriptor[] commands = new AudioCommandDescriptor[0];

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.DelayCommands(duration, commands.Resolve());
	}
}
