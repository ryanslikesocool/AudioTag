// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine.Audio;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.outputAudioMixerGroup"/>.
	/// </summary>
	public readonly struct MixerGroup : IAudioCommand {
		public delegate AudioMixerGroup ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public MixerGroup(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public MixerGroup(AudioMixerGroup value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.outputAudioMixerGroup = valueProvider();
		}
	}
}