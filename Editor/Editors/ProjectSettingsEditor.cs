// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;
using UnityEditor;
using Foundation.Editors;

namespace AudioTag.Editors {
	[CustomEditor(typeof(ProjectSettings))]
	internal sealed class ProjectSettingsEditor : Editor {
		public override void OnInspectorGUI() {
			serializedObject.Update();

			OnPoolGUI();
			OnInstanceGUI();
			OnPreloadGUI();

			serializedObject.ApplyModifiedProperties();
		}

		private void OnInstanceGUI() {
			using (new BoxGroupScope("Instance")) {
			}
		}

		private void OnPoolGUI() {
			using (new BoxGroupScope("Pool")) {
			}
		}

		private void OnPreloadGUI() {
			using (new BoxGroupScope("Preload")) {
				EditorGUILayout.LabelField("Assign audio clips and clip sets to force-enable Preload Audio Data in a build step.");

				SerializedProperty preload = serializedObject.FindProperty("preload");

				EditorGUILayout.PropertyField(preload.FindPropertyRelative("clipSets"));
				EditorGUILayout.PropertyField(preload.FindPropertyRelative("clips"));
			}
		}

		// MARK: - Constants

		public sealed class Styles { }
	}
}