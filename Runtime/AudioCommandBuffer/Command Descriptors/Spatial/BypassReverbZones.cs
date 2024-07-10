// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Spatial/Bypass Reverb Zones",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 10 + 1
	)]
	public sealed class BypassReverbZones : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.BypassReverbZones(value);
	}
}
