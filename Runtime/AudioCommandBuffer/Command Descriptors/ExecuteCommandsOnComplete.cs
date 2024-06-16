// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Linq;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class ExecuteCommandsOnComplete : AudioCommandDescriptor {
		public AudioCommandDescriptor[] commands = new AudioCommandDescriptor[0];

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.ExecuteCommandsOnComplete(commands.Resolve());
	}
}
