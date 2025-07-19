// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.velocityUpdateMode"/>.
	/// </summary>
	public readonly struct VelocityUpdateMode : IAudioCommand {
		public delegate AudioVelocityUpdateMode ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public VelocityUpdateMode(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public VelocityUpdateMode(AudioVelocityUpdateMode value) : this(() => value) { }

		// MARK: - IAudioCommand

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.velocityUpdateMode = valueProvider();
		}
	}
}