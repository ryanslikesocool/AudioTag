// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.spatialBlend"/>.
	/// </summary>
	public readonly struct SpatialBlendAudioCommand : IAudioCommand {
		public delegate float ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public SpatialBlendAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public SpatialBlendAudioCommand(float value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.spatialBlend = valueProvider();
		}
	}
}