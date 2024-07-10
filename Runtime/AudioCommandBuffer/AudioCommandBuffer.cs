// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using System.Linq;
using Foundation;
using UnityEngine;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace AudioTag {
	public partial struct AudioCommandBuffer : ICommandBuffer<AudioCommandBuffer.Context, IAudioCommand> {
		private List<IAudioCommand> commands;

		// MARK: - Lifecycle

		public AudioCommandBuffer(IEnumerable<IAudioCommand> commands) {
			this.commands = commands.ToList();
		}

		// MARK: - Operators

		[MethodImpl(AggressiveInlining)]
		public static implicit operator AudioCommandBuffer(AudioCommandBufferDescriptor descriptor)
			=> descriptor.Resolve();

		[MethodImpl(AggressiveInlining)]
		public static implicit operator AudioCommandBuffer(IAudioCommand[] commands)
			=> new AudioCommandBuffer(commands);

		[MethodImpl(AggressiveInlining)]
		public static implicit operator AudioCommandBuffer(List<IAudioCommand> commands)
			=> new AudioCommandBuffer(commands);

		[MethodImpl(AggressiveInlining)]
		public static implicit operator AudioCommandBuffer(AudioCommandDescriptor[] commands)
			=> new AudioCommandBuffer(commands.Resolve());

		[MethodImpl(AggressiveInlining)]
		public static implicit operator AudioCommandBuffer(List<AudioCommandDescriptor> commands)
			=> new AudioCommandBuffer(commands.Resolve());

		[MethodImpl(AggressiveInlining)]
		public static implicit operator AudioCommandBuffer(AudioCommandDescriptorList commands)
			=> new AudioCommandBuffer(commands.Resolve());

		// MARK: -

		[MethodImpl(AggressiveInlining)]
		public void Add(IAudioCommand command)
			=> (commands ??= new List<IAudioCommand>()).Add(command);

		[MethodImpl(AggressiveInlining)]
		public readonly void Execute(AudioSource instance) {
			Context context = new Context(AudioPool.Shared, instance);

			foreach (IAudioCommand command in commands) {
				command.Execute(ref context);
			}
		}
	}
}