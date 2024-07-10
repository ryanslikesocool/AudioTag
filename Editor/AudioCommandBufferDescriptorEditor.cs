//// Developed With Love by Ryan Boyer https://ryanjboyer.com <3
//
//using UnityEditor;
//using Foundation.Editors;
//using UnityEngine;
//
//namespace AudioTag.Editors {
//	[CustomEditor(typeof(AudioCommandBufferDescriptor)), CanEditMultipleObjects]
//	internal sealed class AudioCommandBufferDescriptorEditor : Editor {
//		private new AudioCommandBufferDescriptor target;
//		private SerializedProperty commands;
//		private AudioCommandListPropertyDrawer drawer;
//
//		private void OnEnable() {
//			target = (AudioCommandBufferDescriptor)base.target;
//			commands = serializedObject.FindProperty("commands");
//			drawer = new AudioCommandListPropertyDrawer { useFoldout = false };
//		}
//
//		// MARK: - GUI
//
//		public override void OnInspectorGUI() {
//			serializedObject.Update();
//
//			FoundationEditorGUI.ScriptField(target);
//
//			Rect rect = EditorGUILayout.GetControlRect(false, drawer.GetPropertyHeight(commands, Style.Commands));
//			drawer.OnGUI(rect, target, commands, Style.Commands);
//
//			serializedObject.ApplyModifiedProperties();
//		}
//
//		// MARK: - Constants
//
//		private static class Style {
//			internal static readonly GUIContent Commands = new GUIContent("Commands");
//		}
//	}
//}