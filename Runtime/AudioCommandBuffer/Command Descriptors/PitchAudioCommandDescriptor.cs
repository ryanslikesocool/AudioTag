namespace AudioTag {
	public sealed class PitchAudioCommandDescriptor : AudioCommandDescriptor {
		public float value;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new PitchAudioCommand(value);
	}
}