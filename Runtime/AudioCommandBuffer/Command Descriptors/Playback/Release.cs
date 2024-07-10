// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Playback/Release",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 16
	)]
	public sealed class Release : AudioCommandDescriptor {
		public override IAudioCommand Resolve()
			=> AudioCommand.Release.Default;
	}
}
