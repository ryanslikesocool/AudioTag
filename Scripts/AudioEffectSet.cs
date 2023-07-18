using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/AudioTag/Audio Effect Set")]
	public sealed class AudioEffectSet : ScriptableObject {
#if ODIN_INSPECTOR_3
		[BoxGroup("Info"), Tooltip("The set's key, used to access the effect set in code.")] public AudioKey key = new AudioKey();
		[Tooltip("Should all of the effects in this set be loaded automatically?")] public bool loadOnLaunch = false;
		[Tooltip("The audio mixer group to output to.  Effect data may override this value.")] public AudioMixerGroup mixerGroup = null;
		[Searchable, ListDrawerSettings(DefaultExpandedState = true)] public AudioEffectData[] data = new AudioEffectData[0];

		[BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool RequiresLoading => data?.Any(data => data.RequiresLoading) ?? false;
		[BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsLoaded => data?.All(data => data.IsLoaded) ?? false;
		[BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsLoading => data?.Any(data => data.IsLoading) ?? false;
		[BoxGroup("Debug"), ShowInInspector, ReadOnly] public bool IsUnloaded => data?.Any(data => data.IsUnloaded) ?? false;
#else
        [Tooltip("The set's key, used to access the effect set in code.")] public AudioTag key = new AudioTag();
        [Tooltip("Should all of the effects in this set be loaded automatically?")] public bool loadOnLaunch = false;
        [Tooltip("The audio mixer group to output to.  Effect data may override this value.")] public AudioMixerGroup mixerGroup = null;
        public AudioEffectData[] data = new AudioEffectData[0];

        public bool RequiresLoading => data?.Any(data => data.RequiresLoading) ?? false;
        public bool IsLoaded => data?.All(data => data.IsLoaded) ?? false;
        public bool IsLoading => data?.Any(data => data.IsLoading) ?? false;
        public bool IsUnloaded => data?.Any(data => data.IsUnloaded) ?? false;
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