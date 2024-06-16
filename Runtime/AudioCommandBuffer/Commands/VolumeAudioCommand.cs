// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.volume"/>.
	/// </summary>
	public readonly struct VolumeAudioCommand : IAudioCommand {
		public delegate float ValueProvider();

		public readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public VolumeAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public VolumeAudioCommand(float volume) : this(() => volume) { }

		public VolumeAudioCommand(float min, float max) : this(() => UnityEngine.Random.Range(min, max)) { }

		public VolumeAudioCommand(ClosedRange<float> range) : this(range.lowerBound, range.upperBound) { }

		// MARK: -

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.volume = valueProvider();
		}
	}
}