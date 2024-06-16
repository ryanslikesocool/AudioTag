using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class RandomClip : AudioCommandDescriptor {
				public AudioClip[] value = new AudioClip[0];

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.Clip(value);
			}
		}
	}
}