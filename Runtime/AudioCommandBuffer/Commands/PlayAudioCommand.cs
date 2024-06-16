// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag {
	public readonly struct PlayAudioCommand : IAudioCommand_Play {
		public delegate float DelayProvider();

		private readonly DelayProvider delayProvider;

		// MARK: - Lifecycle

		public PlayAudioCommand(DelayProvider delayProvider = null) {
			this.delayProvider = delayProvider;
		}

		public PlayAudioCommand(float delay) : this(() => delay) { }

		public PlayAudioCommand(float minDelay, float maxDelay) : this(() => UnityEngine.Random.Range(minDelay, maxDelay)) { }

		public PlayAudioCommand(ClosedRange<float> delayRange) : this(delayRange.lowerBound, delayRange.upperBound) { }

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