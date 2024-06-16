// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class PlayDelayed : AudioCommandDescriptor {
		[Min(float.Epsilon)] public float value = 1.0f;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Play(value);
	}
}
