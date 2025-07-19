// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Listener/Bypass Listener Effects",
		order = 2
	)]
	public sealed class BypassListenerEffects : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.BypassListenerEffects(value);
	}
}
