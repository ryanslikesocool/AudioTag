using ClockKit;
using System.Collections.Generic;
using UnityEngine;

namespace AudioTag.AudioCommand {
	/// <summary>
	/// Delay the execution of commands until the instance is finished playing.
	/// </summary>
	public readonly struct ExecuteCommandsOnComplete : IAudioCommand {
		public delegate IEnumerable<IAudioCommand> ValueProvider();

		public readonly ValueProvider valueProvider;

		// MARK: - Lifecycle

		public ExecuteCommandsOnComplete(ValueProvider valueProvider) {
			this.valueProvider = valueProvider;
		}

		public ExecuteCommandsOnComplete(IEnumerable<IAudioCommand> commands) : this(() => commands) { }

		// MARK: -

		public readonly void Execute(ref AudioCommandBuffer.Context context) {
			AudioCommandBuffer.Context localContext = context;
			IEnumerable<IAudioCommand> localCommands = valueProvider();

			float clipDuration = context.instance.clip.length;
			float speedMultiplier = Mathf.Abs(context.instance.pitch);
			float duration = clipDuration * speedMultiplier;
			CKClock.Delay(seconds: duration, OnComplete);

			void OnComplete() {
				foreach (IAudioCommand item in localCommands) {
					item.Execute(ref localContext);
				}
			}
		}
	}
}