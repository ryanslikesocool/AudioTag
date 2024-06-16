// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.bypassListenerEffects"/>.
	/// </summary>
	public readonly struct BypassListenerEffects : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public BypassListenerEffects(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public BypassListenerEffects(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.bypassListenerEffects = valueProvider();
		}
	}
}
