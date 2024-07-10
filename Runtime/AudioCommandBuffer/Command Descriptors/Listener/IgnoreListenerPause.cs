// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Listener/Ignore Pause",
		order = 0
	)]
	public sealed class IgnoreListenerPause : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.IgnoreListenerPause(value);
	}
}
