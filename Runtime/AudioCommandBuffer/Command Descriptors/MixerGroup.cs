// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine.Audio;

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class MixerGroup : AudioCommandDescriptor {
		public AudioMixerGroup value = null;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.MixerGroup(value);
	}
}
