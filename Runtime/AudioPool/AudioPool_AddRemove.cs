// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
using System.Collections.Generic;
using Foundation;

namespace AudioTag {
	public sealed partial class AudioPool {
		public static void AddSet(in AudioEffectSet set) {
			Shared.setLink.Add(set);
			foreach (AudioDescriptor data in set.data) {
				Shared.AddEffect(data);
			}
		}

		public static void RemoveSet(in AudioEffectSet set) {
			foreach (AudioDescriptor data in set.data) {
				Shared.RemoveEffect(data);
			}
			Shared.setLink.Remove(set);
		}

		private void AddEffect(in AudioDescriptor data) {
			if (prefabLink.ContainsKey(data)) {
				Debug.LogErrorFormat(FORMAT_PREEXISTING_AUDIO_DATA, data.name);
				return;
			}

			AudioInstance prefab = data != null ? data.prefabOverride : this.sourcePrefab;

			prefabLink.Add(data, prefab);
			effectLink.Add(data, new List<AudioInstance>());
		}

		private void RemoveEffect(in AudioDescriptor data) {
			if (!effectLink.TryGetValue(data, out List<AudioInstance> effects)) { return; }

			foreach (AudioInstance e in effects) {
				e.Deinit();
				e.gameObject.DestroySafe();
			}
			effects.Clear();
			effectLink.Remove(data);
			prefabLink.Remove(data);
		}

		// MARK: - Constants

		private const string FORMAT_PREEXISTING_AUDIO_DATA = "The audio data {0} has already been added to the pool.";
	}
}