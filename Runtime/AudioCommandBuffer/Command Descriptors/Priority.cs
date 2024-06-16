using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class PriorityAudioCommandDescriptor : AudioCommandDescriptor {
				[Range(0, 256)] public int value = 128;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new Priority(value);
			}
		}
	}
}