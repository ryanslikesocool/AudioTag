// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.spatialBlend"/>.
	/// </summary>
	public readonly struct SpatialBlend : IAudioCommand {
		public delegate float ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public SpatialBlend(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public SpatialBlend(float value) : this(() => value) { }

		// MARK: - IAudioCommand

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.spatialBlend = valueProvider();
		}
	}
}