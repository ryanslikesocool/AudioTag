// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.reverbZoneMix"/>.
	/// </summary>
	public readonly struct ReverbZoneMixAudioCommand : IAudioCommand {
		public delegate float ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public ReverbZoneMixAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public ReverbZoneMixAudioCommand(float value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.reverbZoneMix = valueProvider();
		}
	}
}