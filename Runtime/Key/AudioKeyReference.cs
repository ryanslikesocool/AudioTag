using UnityEngine;

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/AudioTag/Audio Key")]
	public sealed class AudioKeyReference : ScriptableObject {
		[SerializeField] private string _key = default;
		public string key => _key;

		public static implicit operator AudioKey(AudioKeyReference reference) => new AudioKey(reference._key);
	}
}