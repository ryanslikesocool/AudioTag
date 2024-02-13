// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag {
	public sealed partial class AudioPool {
		// MARK: - Single Data

		public static void Unload(in AudioEffect effect, bool returnToPool = true) {
			effect.Unload();
			if (returnToPool) {
				Return(effect);
			}
		}

		// MARK: - Sets

		public static void LoadSet(in AudioEffectSet set) => LoadSet(set.key);

		public static AudioEffectSet LoadSet(in AudioKey key) {
			if (Shared == null) {
				return null;
			}

			if (Shared.setLink.TryGetValue(key, out AudioEffectSet set)) {
				set.Load();
				return set;
			} else {
				Debug.LogWarning($"AudioEffectSet with key '{key.key}' does not exist.");
				return null;
			}
		}

		public static void UnloadSet(in AudioEffectSet set) => UnloadSet(set.key);

		public static void UnloadSet(in AudioKey key) {
			if (Shared == null) {
				return;
			}

			if (Shared.setLink.TryGetValue(key, out AudioEffectSet set)) {
				set.Unload();
			} else {
				Debug.LogWarning($"AudioEffectSet with key '{key.key}' does not exist.");
			}
		}
	}
}