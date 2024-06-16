// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using Foundation;
using UnityEngine;

namespace AudioTag {
	/// <summary>
	/// A command that changes the value of <see cref="AudioSource.clip"/>.
	/// </summary>
	public readonly struct ClipAudioCommand : IAudioCommand {
		public delegate AudioClip ValueProvider();

		private readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public ClipAudioCommand(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public ClipAudioCommand(AudioClip value) : this(() => value) { }

		public ClipAudioCommand(IList<AudioClip> values) : this(() => values.Random()) { }

		// MARK: -

		public void Execute(ref AudioCommandBuffer.Context context) {
			context.Source.clip = valueProvider();
		}
	}
}