namespace AudioTag {
	public sealed class SpatialBlendAudioCommandDescriptor : AudioCommandDescriptor {
		public float value = 0;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new SpatialBlendAudioCommand(value);
	}
}