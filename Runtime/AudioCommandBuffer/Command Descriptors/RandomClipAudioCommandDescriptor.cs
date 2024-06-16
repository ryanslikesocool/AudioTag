using UnityEngine;

namespace AudioTag {
	public sealed class RandomClipAudioCommandDescriptor : AudioCommandDescriptor {
		public AudioClip[] value;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new ClipAudioCommand(value);
	}
}