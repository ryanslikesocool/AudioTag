// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.ignoreListenerVolume"/>.
	/// </summary>
	public readonly struct IgnoreListenerVolume : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public IgnoreListenerVolume(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public IgnoreListenerVolume(bool value) : this(() => value) { }

		// MARK: - IAudioCommand

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.ignoreListenerVolume = valueProvider();
		}
	}
}