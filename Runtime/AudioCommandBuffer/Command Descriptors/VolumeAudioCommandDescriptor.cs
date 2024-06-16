namespace AudioTag {
	public sealed class VolumeAudioCommandDescriptor : AudioCommandDescriptor {
		public float value = 1;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new VolumeAudioCommand(value);
	}
}