// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class VelocityUpdateMode : AudioCommandDescriptor {
		public AudioVelocityUpdateMode value = AudioVelocityUpdateMode.Auto;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.VelocityUpdateMode(value);
	}
}
