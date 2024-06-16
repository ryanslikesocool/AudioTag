namespace AudioTag {
	public sealed class SpreadAudioCommandDescriptor : AudioCommandDescriptor {
		public float value = 0;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new SpreadAudioCommand(value);
	}
}