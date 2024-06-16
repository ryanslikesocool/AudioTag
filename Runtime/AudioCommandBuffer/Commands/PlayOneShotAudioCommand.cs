// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using Foundation;
using UnityEngine;

namespace AudioTag {
	public readonly struct PlayOneShotAudioCommand : IAudioCommand_Clip, IAudioCommand_Play {
		public delegate AudioClip ClipProvider();
		public delegate float VolumeScaleProvider();

		public readonly ClipProvider clipProvider;
		public readonly VolumeScaleProvider volumeScaleProvider;

		// MARK: - Lifecycle

		public PlayOneShotAudioCommand(ClipProvider clipProvider, VolumeScaleProvider volumeScaleProvider) {
			this.clipProvider = clipProvider;
			this.volumeScaleProvider = volumeScaleProvider;
		}

		public PlayOneShotAudioCommand(AudioClip clip, VolumeScaleProvider volumeScaleProvider) : this(() => clip, volumeScaleProvider) { }

		public PlayOneShotAudioCommand(IList<AudioClip> clips, VolumeScaleProvider volumeScaleProvider) : this(() => clips.Random(), volumeScaleProvider) { }

		public PlayOneShotAudioCommand(AudioClip clip, float volumeScale) : this(() => clip, () => volumeScale) { }

		public PlayOneShotAudioCommand(IList<AudioClip> clips, float volumeScale) : this(() => clips.Random(), () => volumeScale) { }

		public PlayOneShotAudioCommand(AudioClip clip, float minVolumeScale, float maxVolumeScale) : this(() => clip, () => Random.Range(minVolumeScale, maxVolumeScale)) { }

		public PlayOneShotAudioCommand(IList<AudioClip> clips, float minVolumeScale, float maxVolumeScale) : this(() => clips.Random(), () => Random.Range(minVolumeScale, maxVolumeScale)) { }

		public PlayOneShotAudioCommand(AudioClip clip, ClosedRange<float> volumeScaleRange) : this(() => clip, () => Random.Range(volumeScaleRange.lowerBound, volumeScaleRange.upperBound)) { }

		public PlayOneShotAudioCommand(IList<AudioClip> clips, ClosedRange<float> volumeScaleRange) : this(() => clips.Random(), () => Random.Range(volumeScaleRange.lowerBound, volumeScaleRange.upperBound)) { }

		public PlayOneShotAudioCommand(ClipProvider clipProvider, float volumeScale) : this(clipProvider, () => volumeScale) { }

		public PlayOneShotAudioCommand(ClipProvider clipProvider, float minVolumeScale, float maxVolumeScale) : this(clipProvider, () => Random.Range(minVolumeScale, maxVolumeScale)) { }

		public PlayOneShotAudioCommand(ClipProvider clipProvider, ClosedRange<float> volumeScaleRange) : this(clipProvider, () => Random.Range(volumeScaleRange.lowerBound, volumeScaleRange.upperBound)) { }

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