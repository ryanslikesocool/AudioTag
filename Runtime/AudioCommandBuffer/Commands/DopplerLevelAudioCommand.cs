// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.dopplerLevel"/>.
	/// </summary>
	public readonly struct DopplerLevelAudioCommand : IAudioCommand {
		public delegate float ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public DopplerLevelAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public DopplerLevelAudioCommand(float value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.dopplerLevel = valueProvider();
		}
	}
}