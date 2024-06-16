namespace AudioTag {
	public sealed class BypassReverbZonesAudioCommandDescriptor : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new BypassReverbZonesAudioCommand(value);
	}
}