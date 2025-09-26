// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Foundation;
using UnityEngine;
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
			=> new(commands);

		[MethodImpl(AggressiveInlining)]
		public static implicit operator AudioCommandBuffer(List<IAudioCommand> commands)
			=> new(commands);

		[MethodImpl(AggressiveInlining)]
		public static implicit operator AudioCommandBuffer(AudioCommandDescriptor[] commands)
			=> new(commands.Resolve());

		[MethodImpl(AggressiveInlining)]
		public static implicit operator AudioCommandBuffer(List<AudioCommandDescriptor> commands)
			=> new(commands.Resolve());

		[MethodImpl(AggressiveInlining)]
		public static implicit operator AudioCommandBuffer(AudioCommandDescriptorList commands)
			=> new(commands.Resolve());

		// MARK: -

		[MethodImpl(AggressiveInlining)]
		public void Add(IAudioCommand command)
			=> (commands ??= new()).Add(command);

		[MethodImpl(AggressiveInlining)]
		public readonly void Execute(AudioSource instance) {
			Context context = new(AudioPool.Shared, instance);

			foreach (IAudioCommand command in commands) {
				command.Execute(ref context);
			}
		}
	}
}