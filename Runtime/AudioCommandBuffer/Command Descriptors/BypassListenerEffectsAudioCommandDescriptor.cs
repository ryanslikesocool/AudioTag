namespace AudioTag {
	public sealed class BypassListenerEffectsAudioCommandDescriptor : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new BypassListenerEffectsAudioCommand(value);
	}
}