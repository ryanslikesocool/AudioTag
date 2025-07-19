using System.Collections.Generic;
using ClockKit;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// Delay the execution of commands for a number of seconds.
	/// </summary>
	public readonly struct DelayCommands : IAudioCommand {
		public delegate (CKQueue, float, IEnumerable<IAudioCommand>) ValueProvider();

		public readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public DelayCommands(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public DelayCommands(CKQueue queue, float seconds, IEnumerable<IAudioCommand> commands) : this(() => (queue, seconds, commands)) { }

		public DelayCommands(float seconds, IEnumerable<IAudioCommand> commands) : this(queue: CKQueue.Default, seconds: seconds, commands: commands) { }

		// MARK: - IAudioCommand

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			AudioCommandBuffer.Context localContext = context;
			(CKQueue queue, float duration, IEnumerable<IAudioCommand> localCommands) = valueProvider();

			CKClock.Delay(queue: queue, seconds: duration, OnComplete);

			void OnComplete() {
				foreach (IAudioCommand item in localCommands) {
					item.Execute(ref localContext);
				}
			}
		}
	}
}
