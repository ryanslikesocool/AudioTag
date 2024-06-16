// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.panStereo"/>.
	/// </summary>
	public readonly struct StereoPanAudioCommand : IAudioCommand {
		public delegate float ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public StereoPanAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public StereoPanAudioCommand(float value) : this(() => value) { }

		public StereoPanAudioCommand(float min, float max) : this(() => UnityEngine.Random.Range(min, max)) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.panStereo = valueProvider();
		}
	}
}