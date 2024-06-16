// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class RandomTranslation : AudioCommandDescriptor {
		public Space space = Space.Self;
		public Vector3 min = -Vector3.one;
		public Vector3 max = Vector3.one;

		// MARK: -

		public override IAudioCommand Resolve()
			=> AudioCommand.Translation.Random(space, min, max);
	}
}
