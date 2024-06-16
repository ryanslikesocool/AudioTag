using UnityEngine;

namespace AudioTag {
	public abstract class AudioCommandDescriptor : ScriptableObject {
		public abstract IAudioCommand Resolve();
	}
}