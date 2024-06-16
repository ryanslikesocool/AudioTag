// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
using Foundation;

namespace AudioTag {
	public partial struct AudioCommandBuffer {
		public sealed class Context : ICommandBufferContext {
			public AudioDescriptor Descriptor { get; }
			public AudioInstance Instance { get; }

			public AudioSource Source => Instance.Source;
			public bool IsPlaying => Source != null && Source.isPlaying;

			// MARK: - Lifecycle

			internal Context(AudioDescriptor descriptor, AudioInstance instance) {
				this.Descriptor = descriptor;
				this.Instance = instance;
			}

			internal void Complete() {

			}
		}
	}
}