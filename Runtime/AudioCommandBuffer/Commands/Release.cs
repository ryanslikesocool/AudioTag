// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that immediately releases the instance to the pool.
	/// </summary>
	public readonly struct Release : IAudioCommand, IAudioCommand_Release {
		public static readonly Release Default = new Release();

		// MARK: - IAudioCommand

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.pool.Release(context.instance);
		}
	}
}