// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Foundation.Editors;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioEffect))]
	[CanEditMultipleObjects]
	internal class AudioEffectEditor : UnityEditor.Editor {
		private new AudioEffect target;

		public void OnEnable() {
			target = (AudioEffect)base.target;
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();

			InfoGUI();

			serializedObject.ApplyModifiedProperties();
		}

		// MARK: - GUI

		private void InfoGUI() {
			using (new BoxGroupScope(Strings.INFO)) {
				using (new EditorGUI.DisabledGroupScope(true)) {
					EditorGUILayout.TextField(Styles.key, target.Key.key);
					using (new EditorGUILayout.HorizontalScope()) {
						EditorGUILayout.ToggleLeft(Styles.active, target.IsActive);
						EditorGUILayout.ToggleLeft(Styles.isPlaying, target.IsPlaying);
					}
				}
			}
		}

		// MARK: - Styles

		private static class Strings {
			public const string INFO = "Info";
		}

		internal static class Styles {
			public static readonly GUIContent key = new GUIContent("Key", "The key of the data attached to this effect.");
			public static readonly GUIContent active = new GUIContent("Active", "Is the containing GameObject active in the hierarchy?");
			public static readonly GUIContent isPlaying = new GUIContent("Is Playing", "Is the effect currently playing?");
		}
	}
}
#endif