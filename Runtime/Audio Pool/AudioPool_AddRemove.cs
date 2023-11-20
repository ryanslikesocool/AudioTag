// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;
using System.Collections.Generic;

namespace AudioTag {
	public sealed partial class AudioPool {
		public static void AddSet(in AudioEffectSet set) {
			Shared.setLink.Add(set.key, set);
			foreach (AudioEffectData data in set.data) {
				Shared.AddEffect(data);
			}
		}

		public static void RemoveSet(in AudioEffectSet set) {
			foreach (AudioEffectData data in set.data) {
				Shared.RemoveEffect(data);
			}
			Shared.setLink.Remove(set.key);
		}

		private void AddEffect(in AudioEffectData data) {
			AudioKey key = data.key;
			if (prefabLink.ContainsKey(key)) {
				Debug.LogError($"The audio key assigned to {data.name} already exists.  Please rekey one of these objects.  {data.name} will not be added to the link.");
				return;
			}

			AudioEffect prefab = data.prefabOverride ?? this.sourcePrefab;

			prefabLink.Add(key, prefab);
			effectLink.Add(key, new List<AudioEffect>());
		}

		private void RemoveEffect(in AudioEffectData data) {
			AudioKey key = data.key;
			if (!effectLink.TryGetValue(key, out List<AudioEffect> effects)) { return; }

			foreach (AudioEffect e in effects) {
				e.Deinit();
				GameObject.Destroy(e.gameObject);
			}
			effects.Clear();
			effectLink.Remove(key);
			prefabLink.Remove(key);
		}
	}
}