// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.priority"/>.
	/// </summary>
	public readonly struct Priority : IAudioCommand {
		public delegate int ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public Priority(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public Priority(int value) : this(() => value) { }

		// MARK: - IAudioCommand

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.priority = valueProvider();
		}
	}
}