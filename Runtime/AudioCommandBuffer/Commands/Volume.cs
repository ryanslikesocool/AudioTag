// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using Foundation;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// A command that changes the value of <see cref="UnityEngine.AudioSource.volume"/>.
	/// </summary>
	public readonly struct Volume : IAudioCommand {
		public delegate float ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public Volume(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		/// <summary>
		/// Create a command that returns a fixed value.
		/// </summary>
		public Volume(float volume) : this(() => volume) { }

		/// <summary>
		/// Create a command that returns a random value in a range.
		/// </summary>
		[Obsolete("Use `Volume.Random` instead.")]
		public Volume(float min, float max) : this(() => UnityEngine.Random.Range(min, max)) { }

		/// <summary>
		/// Create a command that returns a random value in a range.
		/// </summary>
		[Obsolete("Use `Volume.Random` instead.")]
		public Volume(ClosedRange<float> range) : this(range.lowerBound, range.upperBound) { }

		/// <summary>
		/// Create a command that returns a random value in a range.
		/// </summary>
		public static Volume Random(float min, float max)
			=> new(() => UnityEngine.Random.Range(min, max));

		/// <summary>
		/// Create a command that returns a random value in a range.
		/// </summary>
		public static Volume Random(ClosedRange<float> range)
			=> Random(range.lowerBound, range.upperBound);

		// MARK: -

		/// <summary>
		/// The current value provided by the command.
		/// <para>
		/// This may not return the same value every time, depending on how the command was created.
		/// </para>
		/// </summary>
		public readonly float GetCurrentValue()
			=> valueProvider();

		// MARK: - IAudioCommand

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			context.instance.volume = GetCurrentValue();
		}
	}
}