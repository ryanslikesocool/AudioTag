using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class SpatialBlend : AudioCommandDescriptor {
				[Range(0.0f, 1.0f)] public float value = 0;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.SpatialBlend(value);
			}
		}
	}
}