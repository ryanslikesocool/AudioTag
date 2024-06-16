// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEditor;
using UnityEngine;

namespace AudioTag.Editors {
	internal sealed partial class ProjectSettings : ScriptableObject {
		[SerializeField, Get] private PreloadSettings preload = default;

		// MARK: - Serialization

		internal static ProjectSettings GetOrCreateSettings() {
			const string DEFAULT_SETTINGS_PATH = "Assets/Editor/Xcode Project Settings.asset";

			ProjectSettings settings = LoadExistingSettings();
			if (settings == null) {
				settings = CreateInstance<ProjectSettings>();

				AssetDatabase.CreateAsset(settings, DEFAULT_SETTINGS_PATH);
				AssetDatabase.SaveAssets();
			}
			return settings;
		}

		internal static SerializedObject GetSerializedSettings() {
			return new SerializedObject(GetOrCreateSettings());
		}

		internal static ProjectSettings LoadExistingSettings() {
			string[] guids = AssetDatabase.FindAssets($"t:{typeof(ProjectSettings)}");
			if (guids.Length == 0) {
				return null;
			}
			string path = AssetDatabase.GUIDToAssetPath(guids[0]);
			return AssetDatabase.LoadAssetAtPath<ProjectSettings>(path);
		}

		internal static bool DoesSettingsExists() {
			string[] guids = AssetDatabase.FindAssets($"t:{typeof(ProjectSettings)}");
			return guids.Length > 0;
		}
	}
}