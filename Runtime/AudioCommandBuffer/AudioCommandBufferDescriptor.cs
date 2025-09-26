// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Runtime.CompilerServices;
using UnityEngine;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/Audio/Command Buffer")]
	public partial class AudioCommandBufferDescriptor : ScriptableObject {
		public AudioCommandDescriptorList commands = default;

		// MARK: -

		/// <summary>
		/// Resolve the command buffer descriptor into a runtime instance.
		/// </summary>
		[MethodImpl(AggressiveInlining)]
		public AudioCommandBuffer Resolve()
		 	=> new AudioCommandBuffer(commands.Resolve());
	}
}