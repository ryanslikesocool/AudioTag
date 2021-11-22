// Developed with love by Ryan Boyer http://ryanjboyer.com <3

using System.Linq;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
    [DisallowMultipleComponent]
    public class Pool : MonoBehaviour {
        public static Pool Shared { get; private set; }

#if ODIN_INSPECTOR_3
        [BoxGroup("Info"), SerializeField, Searchable] private Effect[] effects = new Effect[0];
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<int, Effect> prefabLink = null;
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<int, List<Effect>> effectLink = null;
#else
        [SerializeField] private Effect[] effects = new Effect[0];
        private Dictionary<int, Effect> prefabLink = null;
        private Dictionary<int, List<Effect>> effectLink = null;
#endif

        private void Awake() => Shared = this;

        private void OnEnable() {
            prefabLink = new Dictionary<int, Effect>();
            effectLink = new Dictionary<int, List<Effect>>();
            foreach (Effect e in effects) {
                int id = e.Init().ID;
                if (prefabLink.ContainsKey(id)) {
                    Debug.LogError($"The audio ID ({id}) assigned to {e.gameObject.name} already exists on {prefabLink[id]}.  Please rename the tag on one of these objects.  {e.gameObject.name} will not be added to the link.");
                    return;
                }
                prefabLink.Add(id, e);
                effectLink.Add(id, new List<Effect>());
            }
        }

        /// <summary>
        /// Peek the next available Effect with the defined tag.
        /// </summary>
        /// <param name="tag">The tag to look for.</param>
        /// <returns>The Effect with the defined tag, if one was found.</returns>
        public static Effect Peek(string tag) => Peek(tag.GetTagID());

        /// <summary>
        /// Peek the next available Effect with the defined ID.
        /// </summary>
        /// <param name="id">The ID to look for.</param>
        /// <returns>The Effect with the defined ID, if one was found.</returns>
        public static Effect Peek(int id) {
            Effect result = null;
            if (Shared.effectLink.ContainsKey(id)) {
                if (Shared.effectLink[id].Count > 0) {
                    result = Shared.effectLink[id].First(e => e.Active && (!e.Playing || e.isVirtual));
                }
                if (result == null) {
                    result = Instantiate(Shared.prefabLink[id]);
                    Shared.effectLink[id].Add(result);
                }
            }
            return result;
        }
    }
}