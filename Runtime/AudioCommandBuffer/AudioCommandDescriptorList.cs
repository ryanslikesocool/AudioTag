using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AudioTag {
	[Serializable]
	public sealed class AudioCommandDescriptorList {
		[SerializeField] internal AudioCommandDescriptor[] backing;

		// MARK: -

		public IEnumerable<IAudioCommand> Resolve()
			=> backing.Select(command => command.Resolve());

		// MARK: - Collection Access

		public IEnumerable<T> Select<T>()
			where T : AudioCommandDescriptor
		{
			foreach (AudioCommandDescriptor element in backing) {
				if (element is T typedElement) {
					yield return typedElement;
				}
			}
		}

		public bool TryGetFirstCommandDescriptor<T>(out T commandDescriptor)
			where T : AudioCommandDescriptor
		{
			foreach (AudioCommandDescriptor element in backing) {
				if (element is T typedElement) {
					commandDescriptor = typedElement;
					return true;
				}
			}

			commandDescriptor = default;
			return false;
		}

		public bool TryResolveFirstCommand<TDescriptor, TCommand>(out TCommand command)
			where TDescriptor : AudioCommandDescriptor
			where TCommand : IAudioCommand
		{
			try {
				command = ResolveFirstCommand<TDescriptor, TCommand>();
				return true;
			} catch (MissingAudioCommandException) {
				command = default;
				return false;
			}
		}

		public TCommand ResolveFirstCommand<TDescriptor, TCommand>()
			where TDescriptor : AudioCommandDescriptor
			where TCommand : IAudioCommand
		{
			if (!TryGetFirstCommandDescriptor(out TDescriptor commandDescriptor)) {
				throw MissingAudioCommandException.CommandDescriptor<TDescriptor>();
			}
			if (commandDescriptor.Resolve() is not TCommand typedCommand) {
				throw MissingAudioCommandException.Command<TCommand>();
			}
			return typedCommand;
		}
	}
}