// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag.AudioCommand.Descriptor {
	public sealed class HideFlags : AudioCommandDescriptor {
		public UnityEngine.HideFlags value = UnityEngine.HideFlags.HideAndDontSave;

		// MARK: -

		public override IAudioCommand Resolve()
			=> new AudioCommand.HideFlags(value);
	}
}
