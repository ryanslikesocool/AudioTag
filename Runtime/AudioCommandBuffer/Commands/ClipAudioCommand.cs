// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using Foundation;
using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		/// <summary>
		/// A command that changes the value of <see cref="AudioSource.clip"/>.
		/// </summary>
		public readonly struct Clip : IAudioCommand_Clip {
			public delegate AudioClip ValueProvider();

			private readonly ValueProvider valueProvider;

			// MARK: - Lifecycle

			public Clip(ValueProvider valueProvider) {
				this.valueProvider = valueProvider;
			}

			public Clip(AudioClip value) : this(() => value) { }

			public Clip(IList<AudioClip> values) : this(() => values.Random()) { }

			// MARK: -

			public void Execute(ref AudioCommandBuffer.Context context) {
				context.Source.clip = valueProvider();
			}
		}
	}
}