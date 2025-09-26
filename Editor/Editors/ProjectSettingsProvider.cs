// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using Foundation.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AudioTag.Editors {
	internal sealed class ProjectSettingsProvider : SettingsProvider {
		private SerializedObject projectSettings;
		private SerializedObject runtimeProjectSettings;
		private Editor projectSettingsEditor;
		private Editor runtimeProjectSettingsEditor;

		public ProjectSettingsProvider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope) { }

		public override void OnActivate(string searchContext, VisualElement rootElement) {
			projectSettings = GetSerializedSettings<EditorProjectSettings>();
			projectSettingsEditor = Editor.CreateEditor(projectSettings.targetObject);

			runtimeProjectSettings = GetSerializedSettings<RuntimeProjectSettings>();
			runtimeProjectSettingsEditor = Editor.CreateEditor(runtimeProjectSettings.targetObject);
		}

		// MARK: - Draw

		public override void OnGUI(string searchContext) {
			projectSettingsEditor.OnInspectorGUI();
			runtimeProjectSettingsEditor.OnInspectorGUI();

			projectSettings.ApplyModifiedPropertiesWithoutUndo();
			runtimeProjectSettings.ApplyModifiedPropertiesWithoutUndo();
		}

		// MARK: - Serialization

		private static T GetOrCreateSettings<T>() where T : _AudioProjectSettings {
			const string DEFAULT_ASSET_PATH_EDITOR = "Assets/Plugins/Developed With Love/AudioTag/Editor/Audio Project Settings.asset";
			const string DEFAULT_ASSET_PATH_RUNTIME = "Assets/Plugins/Developed With Love/AudioTag/Runtime/Audio Project Settings.asset";

			T settings = LoadExistingSettings<T>();
			if (settings == null) {
				string defaultSettingsPath = typeof(T) switch {
					Type editorType when editorType == typeof(EditorProjectSettings) => DEFAULT_ASSET_PATH_EDITOR,
					Type runtimeType when runtimeType == typeof(RuntimeProjectSettings) => DEFAULT_ASSET_PATH_RUNTIME,
					_ => throw new ArgumentException()
				};
				settings = ScriptableObject.CreateInstance<T>();

				FoundationEditorUtility.CreateDirectory(defaultSettingsPath);

				AssetDatabase.CreateAsset(settings, defaultSettingsPath);
				AssetDatabase.SaveAssets();
			}
			return settings;
		}

		private static SerializedObject GetSerializedSettings<T>() where T : _AudioProjectSettings
			=> new(GetOrCreateSettings<T>());

		private static T LoadExistingSettings<T>() where T : _AudioProjectSettings {
			string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
			if (guids.Length == 0) {
				return null;
			}
			string path = AssetDatabase.GUIDToAssetPath(guids[0]);
			return AssetDatabase.LoadAssetAtPath<T>(path);
		}

		// MARK: - Settings Provider

		[SettingsProvider]
		public static SettingsProvider CreateProjectSettingsProvider() {
			const string MENU_PATH = "Project/Developed With Love/Audio";

			return new ProjectSettingsProvider(MENU_PATH, SettingsScope.Project) {
				keywords = GetSearchKeywordsFromGUIContentProperties<EditorProjectSettingsEditor.Styles>()
			};
		}
	}
}