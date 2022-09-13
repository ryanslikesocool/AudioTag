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
#else
        [Tooltip("The set's tag, used to access the effect set in code.")] public string tag = string.Empty;
        [Tooltip("The internal ID used for runtime access.  Do not reference this value directly, as it may change.")] public int ID => Strings.Add(tag);
        [Tooltip("Should all of the effects in this set be loaded automatically?")] public bool loadOnLaunch = false;
        [Tooltip("The audio mixer group to output to.  Effect data may override this value.")] public AudioMixerGroup mixerGroup = null;
        public AudioEffectData[] data = new AudioEffectData[0];
#endif

        public bool IsLoaded {
            get {
                foreach (AudioEffectData d in data) {
                    if (!d.IsLoaded) {
                        return false;
                    }
                }
                return true;
            }
        }

        public void Load() {
            foreach (AudioEffectData d in data) {
                if (d.RequiresLoading) {
                    d.Load();
                }
                if (d.mixerGroup == null) {
                    d.mixerGroup = mixerGroup;
                }
            }
        }

        public void Unload() {
            foreach (AudioEffectData d in data) {
                if (d.RequiresLoading) {
                    d.Unload();
                }
            }
        }
    }
}