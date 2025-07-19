// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag {
    public sealed partial class AudioPool {
		private static AudioPool _shared = default;

		/// <summary>
		/// The shared instance of the object.
		/// </summary>
		public static AudioPool Shared
			=> _shared ??= new AudioPool();
    }
}
