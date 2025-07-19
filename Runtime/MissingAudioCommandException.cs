using System;

namespace AudioTag {
	public class MissingAudioCommandException : Exception {
		public MissingAudioCommandException() { }

		public MissingAudioCommandException(string message) : base(message) { }

		public MissingAudioCommandException(string message, Exception inner) : base(message, inner) { }

		public static MissingAudioCommandException Command<T>() where T : IAudioCommand
			=> new MissingAudioCommandException(message: $"Failed to access a command of type {typeof(T)}");

		public static MissingAudioCommandException CommandDescriptor<T>() where T : AudioCommandDescriptor
			=> new MissingAudioCommandException(message: $"Failed to access a command descriptor of type {typeof(T)}");
	}
}
