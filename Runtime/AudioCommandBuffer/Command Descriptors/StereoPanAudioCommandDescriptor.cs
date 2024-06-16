namespace AudioTag {
	public sealed class StereoPanAudioCommandDescriptor : AudioCommandDescriptor {
		public float value = 0;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new StereoPanAudioCommand(value);
	}
}