using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class Translation : AudioCommandDescriptor {
				public Space space = Space.Self;
				public Vector3 value = Vector3.zero;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.Translation(space, value);
			}
		}
	}
}