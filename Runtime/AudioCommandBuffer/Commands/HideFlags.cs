// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of the instance's <see cref="GameObject"/>'s hide flags.
	/// </summary>
	public readonly struct HideFlags : IAudioCommand {
		public delegate UnityEngine.HideFlags ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public HideFlags(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public HideFlags(UnityEngine.HideFlags value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.gameObject.hideFlags = valueProvider();
		}
	}
}