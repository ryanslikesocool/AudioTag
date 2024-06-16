// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine.Audio;

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.outputAudioMixerGroup"/>.
	/// </summary>
	public readonly struct MixerAudioCommand : IAudioCommand {
		public delegate AudioMixerGroup ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public MixerAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public MixerAudioCommand(AudioMixerGroup value) : this(() => value) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.outputAudioMixerGroup = valueProvider();
		}
	}
}