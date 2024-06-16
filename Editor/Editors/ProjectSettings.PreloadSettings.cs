// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using UnityEngine;

namespace AudioTag.Editors {
	internal sealed partial class ProjectSettings {
		[Serializable]
		public struct PreloadSettings {
			public AudioClipSet[] clipSets;
			public AudioClip[] clips;
		}
	}
}