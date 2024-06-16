// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class RolloffMode : AudioCommandDescriptor {
		public AudioRolloffMode value = AudioRolloffMode.Logarithmic;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.RolloffMode(value);
	}
}
