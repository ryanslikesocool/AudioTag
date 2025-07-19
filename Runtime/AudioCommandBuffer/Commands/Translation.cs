// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of the audio source's position in the provided space.
	/// </summary>
	public readonly struct Translation : IAudioCommand {
		public delegate (Space, Vector3) ValueProvider();

		public readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public Translation(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		/// <summary>
		/// Create a command that returns a fixed value.
		/// </summary>
		public Translation(Space space, Vector3 volume) : this(() => (space, volume)) { }

		/// <summary>
		/// Create a command that returns a random value in a range.
		/// </summary>
		public static Translation Random(Space space, Vector3 min, Vector3 max)
			=> new Translation(() => (space, Vector3.Lerp(min, max, UnityEngine.Random.Range(0.0f, 1.0f))));

		/// <summary>
		/// Create a command that returns a random value in a range.
		/// </summary>
		public static Translation RandomOnSphere(Space space, Vector3 center, float radius)
			=> new Translation(() => (space, center + UnityEngine.Random.onUnitSphere * radius));

		/// <summary>
		/// Create a command that returns a random value in a range.
		/// </summary>
		public static Translation RandomInSphere(Space space, Vector3 center, float radius)
			=> new Translation(() => (space, center + UnityEngine.Random.insideUnitSphere * radius));

		// MARK: - IAudioCommand

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			(Space space, Vector3 value) = valueProvider();
			switch (space) {
				case Space.World:
					context.instance.transform.position = value;
					break;
				case Space.Self:
					context.instance.transform.localPosition = value;
					break;
			}
		}
	}
}