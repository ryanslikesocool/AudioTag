// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioEffectSet))]
	[CanEditMultipleObjects]
	public class AudioEffectSetEditor : UnityEditor.Editor {
		private new AudioEffectSet target;

		private SerializedProperty key;

		private SerializedProperty loadOnLaunch;
		private SerializedProperty mixerGroup;
		private SerializedProperty data;

		public void OnEnable() {
			target = (AudioEffectSet)base.target;

			key = serializedObject.FindProperty("key");

			loadOnLaunch = serializedObject.FindProperty("loadOnLaunch");
			mixerGroup = serializedObject.FindProperty("mixerGroup");
			data = serializedObject.FindProperty("data");
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();

			InfoGUI();
			GeneralGUI();

			serializedObject.ApplyModifiedProperties();
		}

		// MARK: - GUI

		private void InfoGUI() {
			using (new GroupBox("Info")) {
				EditorGUILayout.PropertyField(key, Styles.key);
				using (new EditorGUI.DisabledGroupScope(true)) {
					EditorGUILayout.Toggle(Styles.requiresLoading, target.RequiresLoading);
					EditorGUILayout.EnumFlagsField(Styles.loadState, target.LoadState);
				}
			}
		}

		private void GeneralGUI() {
			using (new GroupBox("General")) {
				EditorGUILayout.PropertyField(loadOnLaunch, Styles.loadOnLaunch);
				EditorGUILayout.PropertyField(mixerGroup, Styles.mixerGroup);
				EditorGUILayout.PropertyField(data, Styles.data);
			}
		}

		// MARK: - Styles

		private static class Styles {
			internal static readonly GUIContent key = new GUIContent("Key", "The set's key, used to access the set in code.");
			internal static readonly GUIContent requiresLoading = new GUIContent("Requires Loading", "Do any of the audio clips in this set have the \"Load in Background\" flag active?");
			internal static readonly GUIContent loadState = new GUIContent("Load State", "The load states of all clips in this set.");

			internal static readonly GUIContent loadOnLaunch = new GUIContent("Load on Launch", "Should all of the effects in this set be loaded automatically?");
			internal static readonly GUIContent mixerGroup = new GUIContent("Mixer Group", "The audio mixer group to output to.  Individual audio effects may override this value.");
			internal static readonly GUIContent data = new GUIContent("Audio Effect Data", "Effects in this set.");
		}
	}
}
#endif