// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.dopplerLevel"/>.
	/// </summary>
	public readonly struct DopplerLevel : IAudioCommand {
		public delegate float ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public DopplerLevel(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public DopplerLevel(float value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.dopplerLevel = valueProvider();
		}
	}
}