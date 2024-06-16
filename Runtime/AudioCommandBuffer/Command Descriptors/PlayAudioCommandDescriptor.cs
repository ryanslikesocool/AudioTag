namespace AudioTag {
	public sealed class PlayAudioCommandDescriptor : AudioCommandDescriptor {
		public override IAudioCommand Resolve()
			=> new PlayAudioCommand();
	}
}