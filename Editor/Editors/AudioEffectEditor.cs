// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioEffect))]
	[CanEditMultipleObjects]
	public class AudioEffectEditor : UnityEditor.Editor {
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
			using (new GroupBox("Info")) {
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

		private static class Styles {
			internal static readonly GUIContent key = new GUIContent("Key", "The key of the data attached to this effect.");
			internal static readonly GUIContent active = new GUIContent("Active", "Is the containing GameObject active in the hierarchy?");
			internal static readonly GUIContent isPlaying = new GUIContent("Is Playing", "Is the effect currently playing?");
		}
	}
}
#endif