// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class RandomClip : AudioCommandDescriptor {
		public AudioClip[] value = new AudioClip[0];

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Clip(value);
	}
}
