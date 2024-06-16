// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.priority"/>.
	/// </summary>
	public readonly struct PriorityAudioCommand : IAudioCommand {
		public delegate int ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public PriorityAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public PriorityAudioCommand(int value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.priority = valueProvider();
		}
	}
}