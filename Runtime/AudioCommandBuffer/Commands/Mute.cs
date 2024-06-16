// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.mute"/>.
	/// </summary>
	public readonly struct Mute : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public Mute(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public Mute(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.mute = valueProvider();
		}
	}
}