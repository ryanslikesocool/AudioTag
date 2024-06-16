// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.bypassEffects"/>.
	/// </summary>
	public readonly struct BypassEffects : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public BypassEffects(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public BypassEffects(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.bypassEffects = valueProvider();
		}
	}
}