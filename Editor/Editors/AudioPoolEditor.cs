// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioPool))]
	public sealed class AudioPoolEditor : UnityEditor.Editor {
		private SerializedProperty mixer;

		private SerializedProperty sets;
		private SerializedProperty data;

		private SerializedProperty sourcePrefab;
		private SerializedProperty effectHideFlags;

		private SerializedProperty collectionChecks;
		private SerializedProperty defaultCapacity;
		private SerializedProperty maxSize;

		public void OnEnable() {
			mixer = serializedObject.FindProperty("mixer");

			sets = serializedObject.FindProperty("sets");
			data = serializedObject.FindProperty("data");

			sourcePrefab = serializedObject.FindProperty("sourcePrefab");
			effectHideFlags = serializedObject.FindProperty("effectHideFlags");

			collectionChecks = serializedObject.FindProperty("collectionChecks");
			defaultCapacity = serializedObject.FindProperty("defaultCapacity");
			maxSize = serializedObject.FindProperty("maxSize");
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();

			GeneralGUI();
			DataGUI();
			ObjectGUI();
			PoolGUI();

			serializedObject.ApplyModifiedProperties();
		}

		// MARK: - GUI

		private void GeneralGUI() {
			using (new GroupBox()) {
				EditorGUILayout.PropertyField(mixer, Styles.mixer);
			}
		}

		private void DataGUI() {
			using (new GroupBox("Data")) {
				EditorGUILayout.PropertyField(sets, Styles.sets);
				EditorGUILayout.PropertyField(data, Styles.data);
			}
		}

		private void ObjectGUI() {
			using (new GroupBox("Object")) {
				EditorGUILayout.PropertyField(sourcePrefab, Styles.sourcePrefab);
				EditorGUILayout.PropertyField(effectHideFlags, Styles.effectHideFlags);
			}
		}

		private void PoolGUI() {
			using (new GroupBox("Pool")) {
				EditorGUILayout.PropertyField(collectionChecks, Styles.collectionChecks);
				EditorGUILayout.PropertyField(defaultCapacity, Styles.defaultCapacity);
				EditorGUILayout.PropertyField(maxSize, Styles.maxSize);
			}
		}

		// MARK: - Styles

		private static class Styles {
			internal static readonly GUIContent mixer = new GUIContent("Mixer");

			internal static readonly GUIContent sets = new GUIContent("Sets");
			internal static readonly GUIContent data = new GUIContent("Data");

			internal static readonly GUIContent sourcePrefab = new GUIContent("Source Prefab");
			internal static readonly GUIContent effectHideFlags = new GUIContent("Effect Hide Flags");

			internal static readonly GUIContent collectionChecks = new GUIContent("Collection Checks");
			internal static readonly GUIContent defaultCapacity = new GUIContent("Default Capacity");
			internal static readonly GUIContent maxSize = new GUIContent("Max Size");
		}
	}
}
#endif