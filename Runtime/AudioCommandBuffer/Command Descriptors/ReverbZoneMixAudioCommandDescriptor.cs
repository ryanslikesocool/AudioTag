namespace AudioTag {
	public sealed class ReverbZoneMixAudioCommandDescriptor : AudioCommandDescriptor {
		public float value = 1;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new ReverbZoneMixAudioCommand(value);
	}
}