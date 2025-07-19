// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.spread"/>.
	/// </summary>
	public readonly struct Spread : IAudioCommand {
		public delegate float ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public Spread(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public Spread(float value) : this(() => value) { }

		// MARK: - IAudioCommand

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.spread = valueProvider();
		}
	}
}