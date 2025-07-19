// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag {
	public abstract class AudioCommandDescriptor : ScriptableObject {
		/// <summary>
		/// Resolve the descriptor into a runtime instance.
		/// </summary>
		public abstract IAudioCommand Resolve();
	}
}