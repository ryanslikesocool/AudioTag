using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class PlayDelayed : AudioCommandDescriptor {
				[Min(float.Epsilon)] public float value = 1.0f;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.Play(value);
			}
		}
	}
}