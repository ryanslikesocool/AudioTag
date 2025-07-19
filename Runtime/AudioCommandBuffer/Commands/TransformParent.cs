// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of the audio source's transform's parent.
	/// </summary>
	public readonly struct TransformParent : IAudioCommand {
		public delegate (Transform, bool) ValueProvider();

		public readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public TransformParent(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public TransformParent(Transform transform, bool keepWorldPosition = true) : this(() => (transform, keepWorldPosition)) { }

		// MARK: - IAudioCommand

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			(Transform newParent, bool keepWorldPosition) = valueProvider();
			context.instance.transform.SetParent(newParent, keepWorldPosition);
		}
	}
}