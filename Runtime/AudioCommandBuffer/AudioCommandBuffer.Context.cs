// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
using Foundation;

namespace AudioTag {
	public partial struct AudioCommandBuffer {
		public sealed class Context : ICommandBufferContext {
			public readonly AudioPool pool;
			public readonly AudioSource instance;

			// MARK: - Lifecycle

			internal Context(AudioPool pool, AudioSource instance) {
				this.pool = pool;
				this.instance = instance;
			}
		}
	}
}