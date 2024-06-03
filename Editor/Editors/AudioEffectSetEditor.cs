// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Foundation.Editors;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioEffectSet))]
	[CanEditMultipleObjects]
	internal class AudioEffectSetEditor : UnityEditor.Editor {
		private new AudioEffectSet target;

		private SerializedProperty key;

		private SerializedProperty loadOnLaunch;
		private SerializedProperty mixerGroup;
		private SerializedProperty data;

		public void OnEnable() {
			target = (AudioEffectSet)base.target;

			key = serializedObject.FindProperty(Properties.KEY);

			loadOnLaunch = serializedObject.FindProperty(Properties.LOAD_ON_LAUNCH);
			mixerGroup = serializedObject.FindProperty(Properties.MIXER_GROUP);
			data = serializedObject.FindProperty(Properties.DATA);
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();

			InfoGUI();
			GeneralGUI();

			serializedObject.ApplyModifiedProperties();
		}

		// MARK: - GUI

		private void InfoGUI() {
			using (new BoxGroupScope(Strings.INFO)) {
				EditorGUILayout.PropertyField(key, Styles.key);
				using (new EditorGUI.DisabledGroupScope(true)) {
					EditorGUILayout.Toggle(Styles.requiresLoading, target.RequiresLoading);
					EditorGUILayout.EnumFlagsField(Styles.loadState, target.LoadState);
				}
			}
		}

		private void GeneralGUI() {
			using (new BoxGroupScope(Strings.General)) {
				EditorGUILayout.PropertyField(loadOnLaunch, Styles.loadOnLaunch);
				EditorGUILayout.PropertyField(mixerGroup, Styles.mixerGroup);
				EditorGUILayout.PropertyField(data, Styles.data);
			}
		}

		// MARK: - Styles

		internal static class Properties {
			public const string KEY = "key";

			public const string LOAD_ON_LAUNCH = "loadOnLaunch";
			public const string MIXER_GROUP = "mixerGroup";
			public const string DATA = "data";
		}

		internal static class Strings {
			public const string INFO = "Info";
			public const string General = "General";
		}

		internal static class Styles {
			public static readonly GUIContent key = new GUIContent("Key", "The set's key, used to access the set in code.");
			public static readonly GUIContent requiresLoading = new GUIContent("Requires Loading", "Do any of the audio clips in this set have the \"Load in Background\" flag active?");
			public static readonly GUIContent loadState = new GUIContent("Load State", "The load states of all clips in this set.");

			public static readonly GUIContent loadOnLaunch = new GUIContent("Load on Launch", "Should all of the effects in this set be loaded automatically?");
			public static readonly GUIContent mixerGroup = new GUIContent("Mixer Group", "The audio mixer group to output to.  Individual audio effects may override this value.");
			public static readonly GUIContent data = new GUIContent("Audio Effect Data", "Effects in this set.");
		}
	}
}
#endif