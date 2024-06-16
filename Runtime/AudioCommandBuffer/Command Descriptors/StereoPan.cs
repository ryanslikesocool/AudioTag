using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class StereoPan : AudioCommandDescriptor {
				[Range(-1.0f, 1.0f)] public float value = 0;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.StereoPan(value);
			}
		}
	}
}