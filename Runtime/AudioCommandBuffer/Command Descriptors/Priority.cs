// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class Priority : AudioCommandDescriptor {
		[Range(0, 256)] public int value = 128;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Priority(value);
	}
}
