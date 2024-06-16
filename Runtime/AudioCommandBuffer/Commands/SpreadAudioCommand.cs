// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.spread"/>.
	/// </summary>
	public readonly struct SpreadAudioCommand : IAudioCommand {
		public delegate float ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public SpreadAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public SpreadAudioCommand(float value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.spread = valueProvider();
		}
	}
}