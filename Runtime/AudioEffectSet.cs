// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System.Linq;
using Foundation;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioTag {
	/// <summary>
	/// An object grouping multiple AudioEffectData.
	/// </summary>
	/// <seealso cref="AudioEffectData"/>
	[CreateAssetMenu(menuName = "Developed With Love/AudioTag/Audio Effect Set")]
	public class AudioEffectSet : ScriptableObject {
		public AudioKey key = default;
		public bool loadOnLaunch = false;
		public AudioMixerGroup mixerGroup = null;
		public AudioEffectData[] data = new AudioEffectData[0];

		public bool RequiresLoading => data?.Any(data => data.RequiresLoading) ?? false;
		public LoadState LoadState => data?.Reduce(LoadState.None, (result, element) => element.LoadState) ?? LoadState.None;

		// MARK: -

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