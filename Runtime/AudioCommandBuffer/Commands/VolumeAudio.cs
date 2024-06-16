// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.volume"/>.
	/// </summary>
	public readonly struct Volume : IAudioCommand {
		public delegate float ValueProvider();

		public readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public Volume(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public Volume(float volume) : this(() => volume) { }

		public Volume(float min, float max) : this(() => UnityEngine.Random.Range(min, max)) { }

		public Volume(ClosedRange<float> range) : this(range.lowerBound, range.upperBound) { }

		// MARK: -

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.volume = valueProvider();
		}
	}
}