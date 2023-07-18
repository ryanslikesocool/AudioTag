using UnityEngine;

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/AudioTag/Audio Key Reference")]
	public sealed class AudioKeyReference : ScriptableObject {
		[SerializeField] private AudioKey _key = default;
		public AudioKey key => _key;
	}
}