// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.minDistance"/> and <see cref="UnityEngine.AudioSource.maxDistance"/>.
	/// </summary>
	public readonly struct SpatialDistanceRange : IAudioCommand {
		public delegate (float, float) ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public SpatialDistanceRange(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public SpatialDistanceRange(float min, float max) : this(() => (min, max)) { }

		public SpatialDistanceRange(ClosedRange<float> range) : this(range.lowerBound, range.upperBound) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			(context.instance.minDistance, context.instance.maxDistance) = valueProvider();
		}
	}
}