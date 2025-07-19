// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Commands/Delay Commands",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 12
	)]
	public sealed class DelayCommands : AudioCommandDescriptor {
		public float duration = 1.0f;
		public AudioCommandDescriptorList commands = default;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.DelayCommands(duration, commands.Resolve());
	}
}
