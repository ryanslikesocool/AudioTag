// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.minDistance"/> and <see cref="UnityEngine.AudioSource.maxDistance"/>.
	/// </summary>
	public readonly struct SpatialDistanceRangeAudioCommand : IAudioCommand {
		public delegate (float, float) ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public SpatialDistanceRangeAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public SpatialDistanceRangeAudioCommand(float min, float max) : this(() => (min, max)) { }

		public SpatialDistanceRangeAudioCommand(ClosedRange<float> range) : this(range.lowerBound, range.upperBound) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			(context.Source.minDistance, context.Source.maxDistance) = valueProvider();
		}
	}
}