// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.loop"/>.
	/// </summary>
	public readonly struct Loop : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public Loop(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public Loop(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.loop = valueProvider();
		}
	}
}