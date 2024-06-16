// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System.Collections.Generic;
using System.Linq;
using Foundation;

namespace AudioTag {
	public partial struct AudioCommandBuffer : ICommandBuffer<AudioCommandBuffer.Context, IAudioCommand> {
		private List<IAudioCommand> commands;

		// MARK: - Lifecycle

		public AudioCommandBuffer(IEnumerable<IAudioCommand> commands) {
			this.commands = commands.ToList();
		}

		// MARK: -

		public void Add(IAudioCommand command)
			=> (commands ??= new List<IAudioCommand>()).Add(command);

		public readonly void Run(AudioDescriptor descriptor, AudioInstance instance) {
			Context context = new Context(descriptor, instance);

			foreach (IAudioCommand command in commands) {
				command.Execute(ref context);
			}

			context.Complete();
		}
	}
}