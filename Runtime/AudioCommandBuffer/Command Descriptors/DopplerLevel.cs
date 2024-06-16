// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class DopplerLevel : AudioCommandDescriptor {
		[Range(0.0f, 5.0f)] public float value = 1;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.DopplerLevel(value);
	}
}
