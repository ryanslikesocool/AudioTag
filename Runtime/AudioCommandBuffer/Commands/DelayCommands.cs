using ClockKit;
using System.Collections.Generic;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// Delay the execution of commands for a number of seconds.
	/// </summary>
	public readonly struct DelayCommands : IAudioCommand {
		public delegate (float, IEnumerable<IAudioCommand>) ValueProvider();

		public readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public DelayCommands(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public DelayCommands(float seconds, IEnumerable<IAudioCommand> commands) : this(() => (seconds, commands)) { }

		// MARK: -

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			AudioCommandBuffer.Context localContext = context;
			(float duration, IEnumerable<IAudioCommand> localCommands) = valueProvider();

			CKClock.Delay(seconds: duration, OnComplete);

			void OnComplete() {
				foreach (IAudioCommand item in localCommands) {
					item.Execute(ref localContext);
				}
			}
		}
	}
}
