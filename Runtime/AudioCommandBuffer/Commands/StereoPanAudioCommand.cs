// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	public static partial class AudioCommand {
		/// <summary>
		/// A command that changes the value of <see cref="UnityEngine.AudioSource.panStereo"/>.
		/// </summary>
		public readonly struct StereoPan : IAudioCommand {
			public delegate float ValueProvider();

			private readonly ValueProvider valueProvider;

			// MARK: - Lifecycle

			public StereoPan(ValueProvider valueProvider) {
				this.valueProvider = valueProvider;
			}

			public StereoPan(float value) : this(() => value) { }

			public StereoPan(float min, float max) : this(() => UnityEngine.Random.Range(min, max)) { }

			// MARK: -

			public void Execute(ref AudioCommandBuffer.Context context) {
				context.Source.panStereo = valueProvider();
			}
		}
	}
}