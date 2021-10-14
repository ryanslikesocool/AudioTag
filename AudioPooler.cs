using System.Linq;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag
{
    [DisallowMultipleComponent]
    public class AudioPooler : MonoBehaviour
    {
        public static AudioPooler Instance { get; private set; }

#if ODIN_INSPECTOR_3
        [BoxGroup("Info"), SerializeField, Searchable] private AudioEffect[] audioEffects = new AudioEffect[0];
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<int, AudioEffect> prefabLink = null;
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] private Dictionary<int, List<AudioEffect>> effectLink = null;
#else
        [SerializeField] private AudioEffect[] audioEffects = new AudioEffect[0];
        private Dictionary<int, AudioEffect> prefabLink = null;
        private Dictionary<int, List<AudioEffect>> effectLink = null;
#endif

        private void Awake() => Instance = this;

        private void OnEnable()
        {
            prefabLink = new Dictionary<int, AudioEffect>();
            effectLink = new Dictionary<int, List<AudioEffect>>();
            foreach (AudioEffect e in audioEffects)
            {
                int id = e.Init().ID;
                if (prefabLink.ContainsKey(id))
                {
                    Debug.LogError($"The audio ID ({id}) assigned to {e.gameObject.name} already exists on {prefabLink[id]}.  Please rename the tag on one of these objects.  {e.gameObject.name} will not be added to the link.");
                    return;
                }
                prefabLink.Add(id, e);
                effectLink.Add(id, new List<AudioEffect>());
            }
        }

        public static AudioEffect Peek(string tag) => Peek(tag.GetTagID());

        public static AudioEffect Peek(int id)
        {
            AudioEffect result = null;
            if (Instance.effectLink.ContainsKey(id))
            {
                if (Instance.effectLink[id].Count > 0)
                {
                    result = Instance.effectLink[id].First(e => e.Active && (!e.Playing || e.isVirtual));
                }
                if (result == null)
                {
                    result = Instantiate(Instance.prefabLink[id]);
                    Instance.effectLink[id].Add(result);
                }
            }
            return result;
        }
    }
}