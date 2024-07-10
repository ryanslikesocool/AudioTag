using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AudioTag {
	[Serializable]
	public sealed class AudioCommandDescriptorList {
		[SerializeField] private AudioCommandDescriptor[] backing;

		public IEnumerable<IAudioCommand> Resolve()
			=> backing.Select(command => command.Resolve());
	}
}