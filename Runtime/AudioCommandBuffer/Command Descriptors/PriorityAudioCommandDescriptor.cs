namespace AudioTag {
	public sealed class PriorityAudioCommandDescriptor : AudioCommandDescriptor {
		public int value = 128;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new PriorityAudioCommand(value);
	}
}