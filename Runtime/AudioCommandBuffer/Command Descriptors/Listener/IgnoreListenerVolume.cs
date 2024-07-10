// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Listener/Ignore Volume",
		order = 1
	)]
	public sealed class IgnoreListenerVolume : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.IgnoreListenerVolume(value);
	}
}
