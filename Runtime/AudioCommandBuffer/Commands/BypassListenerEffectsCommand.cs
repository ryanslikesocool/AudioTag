// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.bypassListenerEffects"/>.
	/// </summary>
	public readonly struct BypassListenerEffectsAudioCommand : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public BypassListenerEffectsAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public BypassListenerEffectsAudioCommand(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.bypassListenerEffects = valueProvider();
		}
	}
}