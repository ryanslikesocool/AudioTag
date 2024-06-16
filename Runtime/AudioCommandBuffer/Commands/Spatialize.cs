// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.spatialize"/>.
	/// </summary>
	public readonly struct Spatialize : IAudioCommand {
		public delegate bool ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public Spatialize(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public Spatialize(bool value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.spatialize = valueProvider();
		}
	}
}