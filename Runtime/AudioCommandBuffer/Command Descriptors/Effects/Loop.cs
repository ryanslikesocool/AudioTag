// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Effects/Loop",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 2 + 1
	)]
	public sealed class Loop : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Loop(value);
	}
}
