namespace AudioTag {
	public sealed class LoopAudioCommandDescriptor : AudioCommandDescriptor {
		public bool value = false;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new LoopAudioCommand(value);
	}
}