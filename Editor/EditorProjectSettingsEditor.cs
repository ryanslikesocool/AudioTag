// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
using UnityEditor;
using Foundation.Editors;

namespace AudioTag.Editors {
	[CustomEditor(typeof(EditorProjectSettings))]
	internal sealed class EditorProjectSettingsEditor : Editor {
		private SerializedProperty clipSets;
		private SerializedProperty clips;

		// MARK: - Lifecycle

		private void OnEnable() {
			SerializedProperty preload = serializedObject.FindProperty(Property.preload);
			clipSets = preload.FindPropertyRelative(Property.clipSets);
			clips = preload.FindPropertyRelative(Property.clips);
		}

		// MARK: - GUI

		public override void OnInspectorGUI() {
			serializedObject.Update();

			OnPreloadGUI();

			serializedObject.ApplyModifiedProperties();
		}

		private void OnPreloadGUI() {
			using (new FoundationEditorGUI.BoxGroupScope("Preload")) {
				EditorGUILayout.LabelField(Styles.PreloadInfo);

				EditorGUILayout.PropertyField(clipSets);
				EditorGUILayout.PropertyField(clips);
			}
		}

		// MARK: - Constants

		internal sealed class Property {
			public const string preload = "preload";
			public const string clipSets = "clipSets";
			public const string clips = "clips";
		}

		internal sealed class Styles {
			public static readonly GUIContent PreloadInfo = new GUIContent("Assign audio clips and clip sets to force-enable the \"Preload Audio Data\" setting in a build step.");
		}
	}
}