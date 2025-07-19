// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Commands/Execute Commands on Complete",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 12 + 1
	)]
	public sealed class ExecuteCommandsOnComplete : AudioCommandDescriptor {
		public AudioCommandDescriptorList commands = default;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.ExecuteCommandsOnComplete(commands.Resolve());
	}
}
