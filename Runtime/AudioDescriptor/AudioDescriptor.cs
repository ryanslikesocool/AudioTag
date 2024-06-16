// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
using UnityEngine.Audio;

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/DWL Audio/Audio Descriptor")]
	public partial class AudioDescriptor : ScriptableObject {
		public AudioInstance prefabOverride = null;
		public AudioMixerGroup mixerGroup = null;

		public bool loop = false;
		[Range(0, 1)] public float volume = 1;
		[Range(0, 256)] public int priority = 128;

		[Get] private PoolingData pooling = PoolingData.Default;
		[Get] private ClipData clips = ClipData.Default;
		[Get] private PitchData pitch = PitchData.Default;
		[Get] private SpatialData spatial = SpatialData.Default;
	}
}