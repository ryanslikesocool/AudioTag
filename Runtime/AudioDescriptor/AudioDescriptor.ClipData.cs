// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using System.Linq;
using UnityEngine;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace AudioTag {
	public partial class AudioDescriptor {
		[Serializable]
		public struct ClipData : IEnumerable<AudioClip> {
			public AudioClip[] backing;
			public bool random;
			[Min(0)] public int index;

			// MARK: - Properties

			public readonly int Length => backing.Length;
			public readonly bool IsEmpty => Length == 0;

			public readonly bool RequiresLoading => backing.Any(clip => clip != null ? !clip.preloadAudioData : false);
			public readonly LoadState LoadState => backing.Reduce(LoadState.None, (result, element) => {
				if (element != null) {
					return result | element.loadState.ToAudioTag();
				} else {
					return result;
				}
			});

			public readonly float MaxDuration => backing.Max(clip => clip.length);

			// MARK: - Constants

			public static ClipData Default => new ClipData {
				backing = new AudioClip[0],
				random = false,
				index = 0
			};

			// MARK: -

			public readonly AudioClip Get() {
				if (backing.Length == 0) {
					return null;
				}
				if (random) {
					return backing.Random();
				} else {
					return backing[index];
				}
			}

			public readonly void Load() {
				foreach (AudioClip clip in backing) {
					if (clip.loadState == AudioDataLoadState.Unloaded) {
						clip.LoadAudioData();
					}
				}
			}

			public readonly void Unload() {
				foreach (AudioClip clip in backing) {
					if (clip.loadState != AudioDataLoadState.Unloaded) {
						clip.UnloadAudioData();
					}
				}
			}

			// MARK: - IEnumerable

			[MethodImpl(AggressiveInlining)]
			public readonly IEnumerator<AudioClip> GetEnumerator()
				=> ((IEnumerable<AudioClip>)backing).GetEnumerator();

			[MethodImpl(AggressiveInlining)]
			readonly IEnumerator IEnumerable.GetEnumerator()
				=> GetEnumerator();
		}
	}
}