// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System.Linq;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
    [DisallowMultipleComponent]
    public class AudioPool : MonoBehaviour {
        public static AudioPool Shared { get; private set; }

#if ODIN_INSPECTOR_3
        [SerializeField, Searchable] private AudioEffectData[] data = new AudioEffectData[0];
        [SerializeField, Tooltip("The prefab to use by default when creating audio sources.  This cannot be empty.  Assign this to the prefab included in the package folder if a custom one is not needed.")] private AudioEffect sourcePrefab = null;
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<int, AudioEffect> prefabLink = null;
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<int, List<AudioEffect>> effectLink = null;
#else
        [SerializeField] private AudioEffectData[] data = new AudioEffectData[0];
        [SerializeField, Tooltip("The prefab to use by default when creating audio sources.  This cannot be empty.  Assign this to the prefab included in the package folder if a custom one is not needed.")] private AudioEffect sourcePrefab = null;
        private Dictionary<int, AudioEffect> prefabLink = null;
        private Dictionary<int, List<AudioEffect>> effectLink = null;
#endif

        private void Awake() {
            Shared = this;
            Strings.Reset();
        }

        private void OnEnable() {
            prefabLink = new Dictionary<int, AudioEffect>();
            effectLink = new Dictionary<int, List<AudioEffect>>();

            foreach (AudioEffectData d in data) {
                int id = d.ID;
                if (prefabLink.ContainsKey(id)) {
                    Debug.LogError($"The audio ID ('{id}', possibly named '{Strings.Get(id)}') assigned to {d.name} already exists.  Please retag one of these objects.  {d.name} will not be added to the link.");
                    return;
                }

                AudioEffect e = CreatePrefab(d);

                prefabLink.Add(id, e);
                effectLink.Add(id, new List<AudioEffect>());
            }
        }

        private AudioEffect CreatePrefab(AudioEffectData data) {
            AudioEffect prefab = data.prefabOverride == null ? sourcePrefab : data.prefabOverride;
            AudioEffect e = Instantiate(prefab);

            e.Prepare(data);

            return e;
        }

        /// <summary>
        /// Peek the next available AudioEffect with the defined tag.
        /// </summary>
        /// <param name="tag">The tag to look for.</param>
        /// <returns>The AudioEffect with the defined tag, if one was found.</returns>
        public static AudioEffect Peek(string tag) => Peek(Strings.Add(tag));

        /// <summary>
        /// Peek the next available AudioEffect with the defined ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The AudioEffect with the defined ID, if one was found.</returns>
        public static AudioEffect Peek(int id) {
            if (Shared.effectLink.ContainsKey(id)) {
                if (Shared.effectLink[id].Count > 0) {
                    return Shared.effectLink[id].First(e => e.Active && (!e.Playing || e.IsVirtual));
                }

                AudioEffect result = Instantiate(Shared.prefabLink[id]);
                result.Prepare(Shared.prefabLink[id].data);
                Shared.effectLink[id].Add(result);
                return result;
            }

            Debug.LogWarning($"AudioEffect with ID '{id}' (with possible tag '{Strings.Get(id)}') does not exist.");
            return null;
        }
    }
}