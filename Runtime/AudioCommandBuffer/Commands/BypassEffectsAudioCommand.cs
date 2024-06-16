// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.bypassEffects"/>.
	/// </summary>
	public readonly struct BypassEffectsAudioCommand : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public BypassEffectsAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public BypassEffectsAudioCommand(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.bypassEffects = valueProvider();
		}
	}
}