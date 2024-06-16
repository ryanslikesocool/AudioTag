namespace AudioTag {
	public sealed class BypassEffectsAudioCommandDescriptor : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new BypassEffectsAudioCommand(value);
	}
}