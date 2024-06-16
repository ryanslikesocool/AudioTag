// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using Foundation;
using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public readonly struct PlayOneShot : IAudioCommand_Clip, IAudioCommand_Play {
			public delegate AudioClip ClipProvider();
			public delegate float VolumeScaleProvider();

			public readonly ClipProvider clipProvider;
			public readonly VolumeScaleProvider volumeScaleProvider;

			// MARK: - Lifecycle

			public PlayOneShot(ClipProvider clipProvider, VolumeScaleProvider volumeScaleProvider) {
				this.clipProvider = clipProvider;
				this.volumeScaleProvider = volumeScaleProvider;
			}

			public PlayOneShot(AudioClip clip, VolumeScaleProvider volumeScaleProvider) : this(() => clip, volumeScaleProvider) { }

			public PlayOneShot(IList<AudioClip> clips, VolumeScaleProvider volumeScaleProvider) : this(() => clips.Random(), volumeScaleProvider) { }

			public PlayOneShot(AudioClip clip, float volumeScale) : this(() => clip, () => volumeScale) { }

			public PlayOneShot(IList<AudioClip> clips, float volumeScale) : this(() => clips.Random(), () => volumeScale) { }

			public PlayOneShot(AudioClip clip, float minVolumeScale, float maxVolumeScale) : this(() => clip, () => Random.Range(minVolumeScale, maxVolumeScale)) { }

			public PlayOneShot(IList<AudioClip> clips, float minVolumeScale, float maxVolumeScale) : this(() => clips.Random(), () => Random.Range(minVolumeScale, maxVolumeScale)) { }

			public PlayOneShot(AudioClip clip, ClosedRange<float> volumeScaleRange) : this(() => clip, () => Random.Range(volumeScaleRange.lowerBound, volumeScaleRange.upperBound)) { }

			public PlayOneShot(IList<AudioClip> clips, ClosedRange<float> volumeScaleRange) : this(() => clips.Random(), () => Random.Range(volumeScaleRange.lowerBound, volumeScaleRange.upperBound)) { }

			public PlayOneShot(ClipProvider clipProvider, float volumeScale) : this(clipProvider, () => volumeScale) { }

			public PlayOneShot(ClipProvider clipProvider, float minVolumeScale, float maxVolumeScale) : this(clipProvider, () => Random.Range(minVolumeScale, maxVolumeScale)) { }

			public PlayOneShot(ClipProvider clipProvider, ClosedRange<float> volumeScaleRange) : this(clipProvider, () => Random.Range(volumeScaleRange.lowerBound, volumeScaleRange.upperBound)) { }

			// MARK: -

			public void Execute(ref AudioCommandBuffer.Context context) {
				if (volumeScaleProvider == null) {
					context.Source.PlayOneShot(clipProvider());
				} else {
					context.Source.PlayOneShot(clipProvider(), volumeScaleProvider());
				}
			}
		}
	}
}