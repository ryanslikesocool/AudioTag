// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.rolloffMode"/>.
	/// </summary>
	public readonly struct RolloffMode : IAudioCommand {
		public delegate AudioRolloffMode ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public RolloffMode(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public RolloffMode(AudioRolloffMode value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.rolloffMode = valueProvider();
		}
	}
}