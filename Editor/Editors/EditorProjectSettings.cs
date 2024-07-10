// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.Editors {
	internal sealed partial class EditorProjectSettings : _AudioProjectSettings {
		[SerializeField, Get] private PreloadSettings preload = default;
	}
}