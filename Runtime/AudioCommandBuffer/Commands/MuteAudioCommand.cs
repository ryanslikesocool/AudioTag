// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.mute"/>.
	/// </summary>
	public readonly struct MuteAudioCommand : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public MuteAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public MuteAudioCommand(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.mute = valueProvider();
		}
	}
}