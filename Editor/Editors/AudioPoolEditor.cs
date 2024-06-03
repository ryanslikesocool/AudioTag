// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Foundation.Editors;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioPool))]
	internal sealed class AudioPoolEditor : UnityEditor.Editor {
		private SerializedProperty mixer;

		private SerializedProperty sets;
		private SerializedProperty data;

		private SerializedProperty sourcePrefab;
		private SerializedProperty effectHideFlags;

		private SerializedProperty collectionChecks;
		private SerializedProperty defaultCapacity;
		private SerializedProperty maxSize;

		public void OnEnable() {
			mixer = serializedObject.FindProperty(Properties.MIXER);

			sets = serializedObject.FindProperty(Properties.SETS);
			data = serializedObject.FindProperty(Properties.DATA);

			sourcePrefab = serializedObject.FindProperty(Properties.SOURCE_PREFAB);
			effectHideFlags = serializedObject.FindProperty(Properties.EFFECT_HIDE_FLAGS);

			collectionChecks = serializedObject.FindProperty(Properties.COLLECTION_CHECKS);
			defaultCapacity = serializedObject.FindProperty(Properties.DEFAULT_CAPACITY);
			maxSize = serializedObject.FindProperty(Properties.MAX_SIZE);
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
			using (new BoxGroupScope()) {
				EditorGUILayout.PropertyField(mixer, Styles.mixer);
			}
		}

		private void DataGUI() {
			using (new BoxGroupScope(Strings.DATA)) {
				EditorGUILayout.PropertyField(sets, Styles.sets);
				EditorGUILayout.PropertyField(data, Styles.data);
			}
		}

		private void ObjectGUI() {
			using (new BoxGroupScope(Strings.OBJECT)) {
				EditorGUILayout.PropertyField(sourcePrefab, Styles.sourcePrefab);
				EditorGUILayout.PropertyField(effectHideFlags, Styles.effectHideFlags);
			}
		}

		private void PoolGUI() {
			using (new BoxGroupScope(Strings.POOL)) {
				EditorGUILayout.PropertyField(collectionChecks, Styles.collectionChecks);
				EditorGUILayout.PropertyField(defaultCapacity, Styles.defaultCapacity);
				EditorGUILayout.PropertyField(maxSize, Styles.maxSize);
			}
		}

		// MARK: - Styles

		internal static class Properties {
			public const string MIXER = "mixer";

			public const string SETS = "sets";
			public const string DATA = "data";

			public const string SOURCE_PREFAB = "sourcePrefab";
			public const string EFFECT_HIDE_FLAGS = "effectHideFlags";

			public const string COLLECTION_CHECKS = "collectionChecks";
			public const string DEFAULT_CAPACITY = "defaultCapacity";
			public const string MAX_SIZE = "maxSize";
		}

		internal static class Strings {
			public const string DATA = "Data";
			public const string OBJECT = "Object";
			public const string POOL = "Pool";
		}

		internal static class Styles {
			public static readonly GUIContent mixer = new GUIContent("Mixer");

			public static readonly GUIContent sets = new GUIContent("Sets");
			public static readonly GUIContent data = new GUIContent("Data");

			public static readonly GUIContent sourcePrefab = new GUIContent("Source Prefab");
			public static readonly GUIContent effectHideFlags = new GUIContent("Effect Hide Flags");

			public static readonly GUIContent collectionChecks = new GUIContent("Collection Checks");
			public static readonly GUIContent defaultCapacity = new GUIContent("Default Capacity");
			public static readonly GUIContent maxSize = new GUIContent("Max Size");
		}
	}
}
#endif