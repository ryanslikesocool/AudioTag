using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class PitchAudio : AudioCommandDescriptor {
				[Range(-3, 3)] public float value;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new Pitch(value);
			}
		}
	}
}