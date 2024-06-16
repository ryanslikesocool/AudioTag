// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag {
	public static partial class AudioCommand {
		public readonly struct Play : IAudioCommand_Play {
			public delegate float DelayProvider();

			private readonly DelayProvider delayProvider;

			// MARK: - Lifecycle

			public Play(DelayProvider delayProvider = null) {
				this.delayProvider = delayProvider;
			}

			public Play(float delay) : this(() => delay) { }

			public Play(float minDelay, float maxDelay) : this(() => UnityEngine.Random.Range(minDelay, maxDelay)) { }

			public Play(ClosedRange<float> delayRange) : this(delayRange.lowerBound, delayRange.upperBound) { }

			// MARK: -

			public void Execute(ref AudioCommandBuffer.Context context) {
				if (delayProvider == null) {
					context.Source.Play();
				} else {
					context.Source.PlayDelayed(delayProvider());
				}
			}
		}
	}
}