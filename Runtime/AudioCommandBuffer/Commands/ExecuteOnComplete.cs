using System.Collections.Generic;
using ClockKit;
using UnityEngine;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// Delay the execution of commands until the instance is finished playing.
	/// </summary>
	public readonly struct ExecuteCommandsOnComplete : IAudioCommand {
		public delegate (CKQueue, IEnumerable<IAudioCommand>) ValueProvider();

		public readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public ExecuteCommandsOnComplete(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public ExecuteCommandsOnComplete(CKQueue queue, IEnumerable<IAudioCommand> commands) : this(() => (queue, commands)) { }

		public ExecuteCommandsOnComplete(IEnumerable<IAudioCommand> commands) : this(queue: CKQueue.Default, commands: commands) { }

		// MARK: - IAudioCommand

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			AudioCommandBuffer.Context localContext = context;
			(CKQueue queue, IEnumerable<IAudioCommand> localCommands) = valueProvider();

			float clipDuration = context.instance.clip.length;
			float speedMultiplier = Mathf.Abs(context.instance.pitch);
			float duration = clipDuration * speedMultiplier;
			CKClock.Delay(queue: queue, seconds: duration, OnComplete);

			void OnComplete() {
				foreach (IAudioCommand item in localCommands) {
					item.Execute(ref localContext);
				}
			}
		}
	}
}