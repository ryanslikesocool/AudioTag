// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Effects/Bypass Effects",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 2
	)]
	public sealed class BypassEffects : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.BypassEffects(value);
	}
}
