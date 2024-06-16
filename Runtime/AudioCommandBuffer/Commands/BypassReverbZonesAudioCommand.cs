// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.bypassReverbZones"/>.
	/// </summary>
	public readonly struct BypassReverbZonesAudioCommand : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public BypassReverbZonesAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public BypassReverbZonesAudioCommand(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.bypassReverbZones = valueProvider();
		}
	}
}