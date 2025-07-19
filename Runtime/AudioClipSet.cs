// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/Audio/Clip Set")]
	public sealed class AudioClipSet : ScriptableObject, IEnumerable<AudioClip> {
		[SerializeField] private AudioClip[] clips = new AudioClip[0];

		// MARK: - Properties

		public int Length => clips.Length;
		public bool IsEmpty => Length == 0;

		public bool RequiresLoading => clips.Any(clip => clip != null ? !clip.preloadAudioData : false);
		public LoadStateMask LoadState => clips.Aggregate(LoadStateMask.None, (result, element) => {
			if (element != null) {
				return result | element.loadState.ToAudioTag();
			} else {
				return result;
			}
		});

		// MARK: - Load State

		public void Load() {
			foreach (AudioClip clip in clips) {
				clip.LoadAudioData();
			}
		}

		public void Unload() {
			foreach (AudioClip clip in clips) {
				clip.UnloadAudioData();
			}
		}

		// MARK: - IEnumerable

		[MethodImpl(AggressiveInlining)]
		public IEnumerator<AudioClip> GetEnumerator()
			=> ((IEnumerable<AudioClip>)clips).GetEnumerator();

		[MethodImpl(AggressiveInlining)]
		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();
	}
}