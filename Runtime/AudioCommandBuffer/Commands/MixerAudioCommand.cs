// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine.Audio;

namespace AudioTag {
	public static partial class AudioCommand {
		/// <summary>
		/// A command that changes the value of <see cref="UnityEngine.AudioSource.outputAudioMixerGroup"/>.
		/// </summary>
		public readonly struct Mixer : IAudioCommand {
			public delegate AudioMixerGroup ValueProvider();

			private readonly ValueProvider valueProvider;

			// MARK: - Lifecycle

			public Mixer(ValueProvider valueProvider) {
				this.valueProvider = valueProvider;
			}

			public Mixer(AudioMixerGroup value) : this(() => value) { }

			// MARK: -

			public void Execute(ref AudioCommandBuffer.Context context) {
				context.Source.outputAudioMixerGroup = valueProvider();
			}
		}
	}
}