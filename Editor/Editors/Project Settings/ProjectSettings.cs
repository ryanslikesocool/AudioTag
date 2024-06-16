// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AudioTag {
	internal sealed partial class ProjectSettings : ScriptableObject {
#pragma warning disable 0414
		[SerializeField, Get] private AudioClipSet[] preloadSets;
		[SerializeField, Get] private AudioClip[] preloadClips;
#pragma warning restore 0414

		// MARK: - Serialization

		internal static ProjectSettings GetOrCreateSettings() {
			const string DEFAULT_SETTINGS_PATH = "Assets/Plugins/Developed With Love/AudioTag/Audio Settings.asset";

			ProjectSettings settings = LoadExistingSettings();
			if (settings == null) {
				settings = CreateInstance<ProjectSettings>();

				AssetDatabase.CreateAsset(settings, DEFAULT_SETTINGS_PATH);
				AssetDatabase.SaveAssets();
			}
			return settings;
		}

		internal static SerializedObject GetSerializedSettings()
			=> new SerializedObject(GetOrCreateSettings());

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
#endif