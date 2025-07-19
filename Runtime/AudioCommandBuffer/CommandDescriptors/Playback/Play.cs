// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Playback/Play",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 14
	)]
	public sealed class Play : AudioCommandDescriptor {
		public override IAudioCommand Resolve()
			=> new AudioCommand.Play();
	}
}
