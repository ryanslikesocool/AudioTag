// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;

namespace AudioTag {
	internal sealed class ProjectSettingsProvider : SettingsProvider {
		private SerializedObject projectSettings;
		private Editor projectSettingsEditor;

		public ProjectSettingsProvider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope) { }

		public static bool IsSettingsAvailable()
			=> ProjectSettings.DoesSettingsExists();

		public override void OnActivate(string searchContext, VisualElement rootElement) {
			projectSettings = ProjectSettings.GetSerializedSettings();
			projectSettingsEditor = Editor.CreateEditor(projectSettings.targetObject);
		}

		// MARK: - Draw

		public override void OnGUI(string searchContext) {
			projectSettingsEditor.OnInspectorGUI();
			projectSettings.ApplyModifiedPropertiesWithoutUndo();
		}

		// MARK: -

		[SettingsProvider]
		public static SettingsProvider CreateProjectSettingsProvider() {
			const string MENU_PATH = "Project/Developed With Love/Audio";

			ProjectSettingsProvider provider = new ProjectSettingsProvider(MENU_PATH, SettingsScope.Project) {
				keywords = GetSearchKeywordsFromGUIContentProperties<ProjectSettingsEditor.Styles>()
			};
			return provider;
		}
	}
}
#endif