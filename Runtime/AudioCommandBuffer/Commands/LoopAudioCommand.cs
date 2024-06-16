// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.loop"/>.
	/// </summary>
	public readonly struct LoopAudioCommand : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public LoopAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public LoopAudioCommand(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.loop = valueProvider();
		}
	}
}