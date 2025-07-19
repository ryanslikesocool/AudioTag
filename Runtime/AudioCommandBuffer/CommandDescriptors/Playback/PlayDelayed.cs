// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Playback/Play (Delayed)",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 14 + 1
	)]
	public sealed class PlayDelayed : AudioCommandDescriptor {
		[Min(float.Epsilon)] public float value = 1.0f;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.Play(value);
	}
}
