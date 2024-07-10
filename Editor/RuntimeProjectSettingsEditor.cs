// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation.Editors;
using UnityEditor;
using UnityEngine;

namespace AudioTag.Editors {
	[CustomEditor(typeof(RuntimeProjectSettings))]
	internal sealed class RuntimeProjectSettingsEditor : Editor {
		private AudioCommandDescriptorListPropertyDrawer resetCommandBufferDrawer;

		private SerializedProperty resetCommandBuffer;

		private SerializedProperty pool_collectionChecks;
		private SerializedProperty pool_defaultCapacity;
		private SerializedProperty pool_maxSize;

		// MARK: - Lifecycle

		private void OnEnable() {
			resetCommandBufferDrawer = new AudioCommandDescriptorListPropertyDrawer { useFoldout = false };

			resetCommandBuffer = serializedObject.FindProperty(Property.resetCommandBuffer);

			SerializedProperty pool = serializedObject.FindProperty(Property.pool);
			pool_collectionChecks = pool.FindPropertyRelative(Property.collectionChecks);
			pool_defaultCapacity = pool.FindPropertyRelative(Property.defaultCapacity);
			pool_maxSize = pool.FindPropertyRelative(Property.maxSize);
		}

		// MARK: - GUI

		public override void OnInspectorGUI() {
			serializedObject.Update();

			OnPoolGUI();
			OnResetCommandBufferGUI();

			serializedObject.ApplyModifiedProperties();
		}

		private void OnResetCommandBufferGUI() {
			Rect resetCommandBufferDrawerRect = EditorGUILayout.GetControlRect(false, resetCommandBufferDrawer.GetPropertyHeight(resetCommandBuffer, Styles.ResetCommandBuffer));
			resetCommandBufferDrawer.OnGUI(resetCommandBufferDrawerRect, target, resetCommandBuffer, Styles.ResetCommandBuffer);
		}

		private void OnPoolGUI() {
			using (new FoundationEditorGUI.BoxGroupScope(Styles.Pool)) {
				EditorGUILayout.PropertyField(pool_collectionChecks, Styles.Pool_CollectionChecks);
				EditorGUILayout.PropertyField(pool_defaultCapacity, Styles.Pool_DefaultCapacity);
				EditorGUILayout.PropertyField(pool_maxSize, Styles.Pool_MaxSize);
			}
		}

		// MARK: - Constants

		private sealed class Property {
			internal const string resetCommandBuffer = "resetCommandBuffer";

			internal const string pool = "pool";
			internal const string collectionChecks = "collectionChecks";
			internal const string defaultCapacity = "defaultCapacity";
			internal const string maxSize = "maxSize";
		}

		internal sealed class Styles {
			internal static readonly GUIContent ResetCommandBuffer = new GUIContent("Reset Command Buffer");
			internal static readonly GUIContent ResetCommandBuffer_Info = new GUIContent("Declare default values to be applied when resetting an audio instance.");

			internal static readonly GUIContent Pool = new GUIContent("Pool");
			internal static readonly GUIContent Pool_CollectionChecks = new GUIContent("Collection Checks");
			internal static readonly GUIContent Pool_DefaultCapacity = new GUIContent("Default Capacity");
			internal static readonly GUIContent Pool_MaxSize = new GUIContent("Max Size");
		}
	}
}