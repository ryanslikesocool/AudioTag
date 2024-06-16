using System.Linq;
using UnityEngine;

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/DWL Audio/Audio Command Buffer")]
	public partial class AudioCommandBufferDescriptor : ScriptableObject {
		private AudioCommandDescriptor[] commands = new AudioCommandDescriptor[0];

		// MARK: -

		public AudioCommandBuffer CreateCommandBuffer()
		 	=> new AudioCommandBuffer(commands.Select(command => command.Resolve()));
	}
}