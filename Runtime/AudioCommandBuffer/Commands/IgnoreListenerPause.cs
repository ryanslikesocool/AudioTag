// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.ignoreListenerPause"/>.
	/// </summary>
	public readonly struct IgnoreListenerPause : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public IgnoreListenerPause(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public IgnoreListenerPause(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.ignoreListenerPause = valueProvider();
		}
	}
}
