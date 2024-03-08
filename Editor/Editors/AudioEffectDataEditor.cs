// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioEffectData))]
	[CanEditMultipleObjects]
	public class AudioEffectDataEditor : UnityEditor.Editor {
		private new AudioEffectData target;

		private SerializedProperty key;

		private SerializedProperty prefabOverride;
		private SerializedProperty mixerGroup;
		private SerializedProperty isVirtual;
		//private SerializedProperty attachmentMode;
		private SerializedProperty loop;
		private SerializedProperty volume;
		private SerializedProperty priority;

		private SerializedProperty clips;
		private SerializedProperty randomClip;
		private SerializedProperty clipIndex;

		private SerializedProperty randomPitch;
		private SerializedProperty fixedPitch;
		private SerializedProperty pitchRange;

		private SerializedProperty spatialBlend;
		private SerializedProperty reverbZoneMix;
		private SerializedProperty dopplerLevel;
		private SerializedProperty spread;
		//private SerializedProperty distanceRange;
		private SerializedProperty minDistance;
		private SerializedProperty maxDistance;

		public void OnEnable() {
			target = (AudioEffectData)base.target;

			key = serializedObject.FindProperty("key");

			prefabOverride = serializedObject.FindProperty("prefabOverride");
			mixerGroup = serializedObject.FindProperty("mixerGroup");
			isVirtual = serializedObject.FindProperty("isVirtual");
			//attachmentMode = serializedObject.FindProperty("attachmentMode");
			loop = serializedObject.FindProperty("loop");
			volume = serializedObject.FindProperty("volume");
			priority = serializedObject.FindProperty("priority");

			clips = serializedObject.FindProperty("clips");
			randomClip = serializedObject.FindProperty("randomClip");
			clipIndex = serializedObject.FindProperty("clipIndex");

			randomPitch = serializedObject.FindProperty("randomPitch");
			fixedPitch = serializedObject.FindProperty("fixedPitch");
			pitchRange = serializedObject.FindProperty("pitchRange");

			spatialBlend = serializedObject.FindProperty("spatialBlend");
			reverbZoneMix = serializedObject.FindProperty("reverbZoneMix");
			dopplerLevel = serializedObject.FindProperty("dopplerLevel");
			spread = serializedObject.FindProperty("spread");
			//distanceRange = serializedObject.FindProperty("distanceRange");
			minDistance = serializedObject.FindProperty("minDistance");
			maxDistance = serializedObject.FindProperty("maxDistance");
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();

			InfoGUI();
			GeneralGUI();
			ClipGUI();
			PitchGUI();
			SpatialGUI();

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
				EditorGUILayout.PropertyField(prefabOverride, Styles.prefabOverride);
				EditorGUILayout.PropertyField(mixerGroup, Styles.mixerGroup);
				EditorGUILayout.PropertyField(isVirtual, Styles.isVirtual);
				//EditorGUILayout.PropertyField(attachmentMode, Styles.attachmentMode);
				EditorGUILayout.PropertyField(loop, Styles.loop);
				EditorGUILayout.PropertyField(volume, Styles.volume);
				EditorGUILayout.PropertyField(priority, Styles.priority);
			}
		}

		private void ClipGUI() {
			using (new GroupBox("Clip")) {
				EditorGUILayout.PropertyField(clips, Styles.clips);
				using (new EditorGUI.DisabledGroupScope(clips.arraySize < 2)) {
					EditorGUILayout.PropertyField(randomClip, Styles.randomClip);
				}
				using (new EditorGUI.DisabledGroupScope(randomClip.boolValue || clips.arraySize < 2)) {
					EditorGUILayout.PropertyField(clipIndex, Styles.clipIndex);
				}
			}
		}

		private void PitchGUI() {
			using (new GroupBox("Pitch")) {
				EditorGUILayout.PropertyField(randomPitch, Styles.randomPitch);
				using (new EditorGUI.DisabledGroupScope(randomPitch.boolValue)) {
					EditorGUILayout.PropertyField(fixedPitch, Styles.fixedPitch);
				}
				using (new EditorGUI.DisabledGroupScope(!randomPitch.boolValue)) {
					EditorGUILayout.PropertyField(pitchRange, Styles.pitchRange);
				}
			}
		}

		private void SpatialGUI() {
			using (new GroupBox("Spatial")) {
				EditorGUILayout.PropertyField(spatialBlend);
				using (new EditorGUI.DisabledGroupScope(spatialBlend.floatValue <= 0)) {
					EditorGUILayout.PropertyField(reverbZoneMix);
					EditorGUILayout.PropertyField(dopplerLevel);
					EditorGUILayout.PropertyField(spread);
					//EditorGUILayout.PropertyField(distanceRange);
					EditorGUILayout.PropertyField(minDistance);
					EditorGUILayout.PropertyField(maxDistance);
				}
			}
		}

		// MARK: - Styles

		internal static class Styles {
			internal static readonly GUIContent key = new GUIContent("Key", "The effect's key, used to access the audio effect in code.");
			internal static readonly GUIContent requiresLoading = new GUIContent("Requires Loading", "Do any of the audio clips in this effect have the \"Load in Background\" flag active?");
			internal static readonly GUIContent loadState = new GUIContent("Load State", "The load states of all clips in this effect.");

			internal static readonly GUIContent prefabOverride = new GUIContent("Prefab Override", "Use this prefab when playing the effect instead of the default.");
			internal static readonly GUIContent mixerGroup = new GUIContent("Mixer Group", "The Audio Mixer Group to output to.  Setting this will override the value in a containing AudioEffectSet.");
			internal static readonly GUIContent isVirtual = new GUIContent("Is Virtual", "Should the Audio Source be marked virtual?");
			//internal static readonly GUIContent attachmentMode = new GUIContent("Attachment Mode", "The attachment mode for the effect.  \"Detached\" does not require a GameObject.");
			internal static readonly GUIContent loop = new GUIContent("Loop", "Should the effect loop?");
			internal static readonly GUIContent volume = new GUIContent("Volume", "The volume of the effect.");
			internal static readonly GUIContent priority = new GUIContent("Priority", "The priority of the effect.  When the system is under heavy load, lower priority effects may be ignored.");

			internal static readonly GUIContent clips = new GUIContent("Clips", "The available Audio Clips for this effect.");
			internal static readonly GUIContent randomClip = new GUIContent("Random Clip", "Should a random clip be selected from the Clips list?.  This is only available when 2 or more clips are in the list.");
			internal static readonly GUIContent clipIndex = new GUIContent("Clip Index", "The index of the clip to be played.  This is only available when Random Clip is off and there are 2 or more clips in the list.");

			internal static readonly GUIContent randomPitch = new GUIContent("Random Pitch", "Should a random pitch be used each time the effect is played?  A value of 1 will play the effect as normal.");
			internal static readonly GUIContent fixedPitch = new GUIContent("Fixed Pitch", "Used a fixed pitch each time this effect is played.  This is only available when Random Pitch is off.");
			internal static readonly GUIContent pitchRange = new GUIContent("Pitch Range", "The range of pitches to use.  This is only available when Random Pitch is on.");
		}
	}
}
#endif