namespace AudioTag {
	public sealed class MuteAudioCommandDescriptor : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new MuteAudioCommand(value);
	}
}