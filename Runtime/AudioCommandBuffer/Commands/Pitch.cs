// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.pitch"/>.
	/// </summary>
	public readonly struct Pitch : IAudioCommand {
		public delegate float ValueProvider();

		public readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public Pitch(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public Pitch(float volume) : this(() => volume) { }

		public Pitch(float min, float max) : this(() => UnityEngine.Random.Range(min, max)) { }

		public Pitch(ClosedRange<float> range) : this(range.lowerBound, range.upperBound) { }

		// MARK: -

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.pitch = valueProvider();
		}
	}
}