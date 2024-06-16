using UnityEngine.Audio;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class Mixer : AudioCommandDescriptor {
				public AudioMixerGroup value = null;

				// MARK: -

				public override IAudioCommand Resolve()
					=> new AudioCommand.Mixer(value);
			}
		}
	}
}