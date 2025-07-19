// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.Editors {
	internal sealed partial class EditorProjectSettings : _AudioProjectSettings {
		[SerializeField] private PreloadSettings preload = default;

		// MARK: - Properties
		
		public PreloadSettings Preload => preload;
	}
}