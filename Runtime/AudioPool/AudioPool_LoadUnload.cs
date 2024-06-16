// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

namespace AudioTag {
	public sealed partial class AudioPool {
		public static void Unload(in AudioInstance effect, bool returnToPool = true) {
			effect.Unload();
			if (returnToPool) {
				Return(effect);
			}
		}
	}
}