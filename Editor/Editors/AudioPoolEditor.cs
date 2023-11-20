#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioPool))]
	public sealed class AudioPoolEditor : UnityEditor.Editor {
		private const string BOX_STYLE = "HelpBox";

		public override void OnInspectorGUI() {
			using (new EditorGUI.DisabledScope(true)) {
				EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((MonoBehaviour)target), GetType(), false);
			}

			serializedObject.Update();

			using (new EditorGUILayout.VerticalScope(BOX_STYLE)) {
				EditorGUILayout.PropertyField(serializedObject.FindProperty("mixer"));
			}

			using (new EditorGUILayout.VerticalScope(BOX_STYLE)) {
				EditorGUILayout.LabelField("Data", EditorStyles.boldLabel);
				using (new EditorGUI.IndentLevelScope()) {
					EditorGUILayout.PropertyField(serializedObject.FindProperty("sets"));
					EditorGUILayout.PropertyField(serializedObject.FindProperty("data"));
				}
			}

			using (new EditorGUILayout.VerticalScope(BOX_STYLE)) {
				EditorGUILayout.LabelField("Object", EditorStyles.boldLabel);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("sourcePrefab"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("effectHideFlags"));
			}

			using (new EditorGUILayout.VerticalScope(BOX_STYLE)) {
				EditorGUILayout.LabelField("Pooling", EditorStyles.boldLabel);
				EditorGUILayout.PropertyField(serializedObject.FindProperty("collectionChecks"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("defaultCapacity"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("maxSize"));
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif