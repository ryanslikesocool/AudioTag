using UnityEngine;

namespace AudioTag {
	public abstract class AudioCommandDescriptor : ScriptableObject {
		/// <summary>
		/// Resolve the descriptor into a runtime instance.
		/// </summary>
		public abstract IAudioCommand Resolve();
	}
}