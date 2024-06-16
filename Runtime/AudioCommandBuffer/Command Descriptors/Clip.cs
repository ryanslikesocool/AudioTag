using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class Clip : AudioCommandDescriptor {
				public AudioClip value = null;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.Clip(value);
			}
		}
	}
}