// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using UnityEngine;

namespace AudioTag {
	public sealed partial class RuntimeProjectSettings {
		[Serializable]
		public struct InstanceSettings {
			public HideFlags instanceHideFlags;
			public AudioCommandDescriptor[] resetCommandBuffer;
		}
	}
}
