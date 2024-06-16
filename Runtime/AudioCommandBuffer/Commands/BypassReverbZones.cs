// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.bypassReverbZones"/>.
	/// </summary>
	public readonly struct BypassReverbZones : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public BypassReverbZones(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public BypassReverbZones(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.bypassReverbZones = valueProvider();
		}
	}
}