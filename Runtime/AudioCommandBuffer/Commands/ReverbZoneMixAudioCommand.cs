// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	public static partial class AudioCommand {
		/// <summary>
		/// A command that changes the value of <see cref="UnityEngine.AudioSource.reverbZoneMix"/>.
		/// </summary>
		public readonly struct ReverbZoneMix : IAudioCommand {
			public delegate float ValueProvider();

			private readonly ValueProvider valueProvider;

			// MARK: - Lifecycle

			public ReverbZoneMix(ValueProvider valueProvider) {
				this.valueProvider = valueProvider;
			}

			public ReverbZoneMix(float value) : this(() => value) { }

			// MARK: -

			public void Execute(ref AudioCommandBuffer.Context context) {
				context.Source.reverbZoneMix = valueProvider();
			}
		}
	}
}