// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Spatial/Rolloff Mode",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 8 + 5
	)]
	public sealed class RolloffMode : AudioCommandDescriptor {
		public AudioRolloffMode value = AudioRolloffMode.Logarithmic;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.RolloffMode(value);
	}
}
