using UnityEngine;

namespace AudioTag {
	public sealed class ClipAudioCommandDescriptor : AudioCommandDescriptor {
		public AudioClip value = null;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new ClipAudioCommand(value);
	}
}