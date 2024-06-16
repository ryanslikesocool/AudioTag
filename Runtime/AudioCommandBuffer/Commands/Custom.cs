// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that runs a custom block of code.
	/// </summary>
	public readonly struct Custom : IAudioCommand {
		public delegate void Body(ref AudioCommandBuffer.Context context);

		private readonly Body body;

		// MARK: - Lifecycle

		public Custom(Body body) {
			this.body = body;
		}

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			body(ref context);
		}
	}
}