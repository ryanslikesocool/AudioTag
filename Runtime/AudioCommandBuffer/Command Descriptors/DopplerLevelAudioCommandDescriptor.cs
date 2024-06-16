namespace AudioTag {
	public sealed class DopplerLevelAudioCommandDescriptor : AudioCommandDescriptor {
		public float value = 1;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new DopplerLevelAudioCommand(value);
	}
}