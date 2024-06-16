// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class PlayRandomDelayed : AudioCommandDescriptor {
		public ClosedRange<float> value = new ClosedRange<float>(0.75f, 1.25f);

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Play(value);
	}
}
