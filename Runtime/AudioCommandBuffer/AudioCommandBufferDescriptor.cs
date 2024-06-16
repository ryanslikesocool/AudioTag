// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Linq;
using UnityEngine;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace AudioTag {
	[CreateAssetMenu(menuName = "Developed With Love/Audio/Command Buffer")]
	public partial class AudioCommandBufferDescriptor : ScriptableObject {
		[SerializeField, Get] private AudioCommandDescriptor[] commands = new AudioCommandDescriptor[0];

		// MARK: -

		/// <summary>
		/// Resolve the command buffer descriptor into a runtime instance.
		/// </summary>
		[MethodImpl(AggressiveInlining)]
		public AudioCommandBuffer Resolve()
		 	=> new AudioCommandBuffer(commands.Select(command => command.Resolve()));
	}
}