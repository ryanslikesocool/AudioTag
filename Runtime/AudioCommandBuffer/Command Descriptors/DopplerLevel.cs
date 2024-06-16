using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class DopplerLevel : AudioCommandDescriptor {
				[Range(0.0f, 5.0f)] public float value = 1;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.DopplerLevel(value);
			}
		}
	}
}