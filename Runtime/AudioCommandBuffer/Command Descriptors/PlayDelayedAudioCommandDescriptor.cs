namespace AudioTag {
	public sealed class PlayDelayedAudioCommandDescriptor : AudioCommandDescriptor {
		public float value = 1.0f;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new PlayAudioCommand(value);
	}
}