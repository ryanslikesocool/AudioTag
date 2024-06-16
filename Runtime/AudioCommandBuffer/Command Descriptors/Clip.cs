// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class Clip : AudioCommandDescriptor {
		public AudioClip value = null;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Clip(value);
	}
}
