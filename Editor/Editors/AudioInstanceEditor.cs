// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Foundation.Editors;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioInstance))]
	[CanEditMultipleObjects]
	internal class AudioEffectEditor : UnityEditor.Editor {
		private new AudioInstance target;

		public void OnEnable() {
			target = (AudioInstance)base.target;
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
					EditorGUILayout.ObjectField(Styles.data, target.Data, typeof(AudioDescriptor), allowSceneObjects: false);
					EditorGUILayout.Toggle(Styles.isPlaying, target.IsPlaying);
				}
			}
		}

		// MARK: - Styles

		private static class Strings {
			public const string INFO = "Info";
		}

		internal static class Styles {
			public static readonly GUIContent data = new GUIContent("Data", "The data playing on the audio effect");
			public static readonly GUIContent isPlaying = new GUIContent("Is Playing", "Is the effect currently playing?");
		}
	}
}
#endif