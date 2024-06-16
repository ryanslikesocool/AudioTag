// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.pitch"/>.
	/// </summary>
	public readonly struct PitchAudioCommand : IAudioCommand {
		public delegate float ValueProvider();

		public readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public PitchAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public PitchAudioCommand(float volume) : this(() => volume) { }

		public PitchAudioCommand(float min, float max) : this(() => UnityEngine.Random.Range(min, max)) { }

		public PitchAudioCommand(ClosedRange<float> range) : this(range.lowerBound, range.upperBound) { }

		// MARK: -

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.pitch = valueProvider();
		}
	}
}