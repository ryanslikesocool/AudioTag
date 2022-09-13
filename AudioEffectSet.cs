using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
    [CreateAssetMenu(menuName = "Developed With Love/AudioTag/Audio Effect Set")]
    public class AudioEffectSet : ScriptableObject {
#if ODIN_INSPECTOR_3
        [BoxGroup("Info"), Tooltip("The set's tag, used to access the effect set in code.")] public string tag = string.Empty;
        [BoxGroup("Info"), Tooltip("The internal ID used for runtime access.  Do not reference this value directly, as it may change."), ShowInInspector, ReadOnly] public int ID => Strings.Add(tag);
        [Tooltip("Should all of the effects in this set be loaded automatically?")] public bool loadOnLaunch = false;
        [Tooltip("The audio mixer group to output to.  Effect data may override this value.")] public AudioMixerGroup mixerGroup = null;
        [Searchable, ListDrawerSettings(Expanded = true)] public AudioEffectData[] data = new AudioEffectData[0];

        [BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool RequiresLoading => data.Any(data => data.RequiresLoading);
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsLoaded => data.All(data => data.IsLoaded);
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsLoading => data.Any(data => data.IsLoading);
        [BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsUnloaded => data.Any(data => data.IsUnloaded);
#else
        [Tooltip("The set's tag, used to access the effect set in code.")] public string tag = string.Empty;
        [Tooltip("The internal ID used for runtime access.  Do not reference this value directly, as it may change.")] public int ID => Strings.Add(tag);
        [Tooltip("Should all of the effects in this set be loaded automatically?")] public bool loadOnLaunch = false;
        [Tooltip("The audio mixer group to output to.  Effect data may override this value.")] public AudioMixerGroup mixerGroup = null;
        public AudioEffectData[] data = new AudioEffectData[0];

        public bool RequiresLoading => data.Any(data => data.RequiresLoading);
        public bool IsLoaded => data.All(data => data.IsLoaded);
        public bool IsLoading => data.Any(data => data.IsLoading);
        public bool IsUnloaded => data.Any(data => data.IsUnloaded);
#endif

        public void Load() {
            foreach (AudioEffectData d in data) {
                d.Load();

                if (d.mixerGroup == null) {
                    d.mixerGroup = mixerGroup;
                }
            }
        }

        public void Unload() {
            foreach (AudioEffectData d in data) {
                d.Unload();
            }
        }
    }
}