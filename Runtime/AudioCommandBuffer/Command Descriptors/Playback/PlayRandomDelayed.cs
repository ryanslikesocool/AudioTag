// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Playback/Play (Random, Delayed)",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 14 + 2
	)]
	public sealed class PlayRandomDelayed : AudioCommandDescriptor {
		public ClosedRange<float> value = new ClosedRange<float>(0.75f, 1.25f);

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Play(value);
	}
}
