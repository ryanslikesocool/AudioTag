// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections;
using System.Collections.Generic;
using Foundation;
using System.Linq;
using UnityEngine;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/DWL Audio/Audio Clip Set")]
	public sealed class AudioClipSet : ScriptableObject, IEnumerable<AudioClip> {
		[SerializeField] private AudioClip[] clips = new AudioClip[0];

		// MARK: - Properties

		public int Length => clips.Length;
		public bool IsEmpty => Length == 0;

		public bool RequiresLoading => clips.Any(clip => clip != null ? !clip.preloadAudioData : false);
		public LoadState LoadState => clips.Reduce(LoadState.None, (result, element) => {
			if (element != null) {
				return result | element.loadState.ToAudioTag();
			} else {
				return result;
			}
		});

		// MARK: - IEnumerable

		[MethodImpl(AggressiveInlining)]
		public IEnumerator<AudioClip> GetEnumerator()
			=> ((IEnumerable<AudioClip>)clips).GetEnumerator();

		[MethodImpl(AggressiveInlining)]
		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();
	}
}