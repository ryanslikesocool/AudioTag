// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Foundation.Editors;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioEffectData))]
	[CanEditMultipleObjects]
	internal class AudioEffectDataEditor : UnityEditor.Editor {
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

			key = serializedObject.FindProperty(Properties.KEY);

			prefabOverride = serializedObject.FindProperty(Properties.PREFAB_OVERRIDE);
			mixerGroup = serializedObject.FindProperty(Properties.MIXER_GROUP);
			isVirtual = serializedObject.FindProperty(Properties.IS_VIRTUAL);
			//attachmentMode = serializedObject.FindProperty(Properties.ATTACHMENT_MODE);
			loop = serializedObject.FindProperty(Properties.LOOP);
			volume = serializedObject.FindProperty(Properties.VOLUME);
			priority = serializedObject.FindProperty(Properties.PRIORITY);

			clips = serializedObject.FindProperty(Properties.CLIPS);
			randomClip = serializedObject.FindProperty(Properties.RANDOM_CLIP);
			clipIndex = serializedObject.FindProperty(Properties.CLIP_INDEX);

			randomPitch = serializedObject.FindProperty(Properties.RANDOM_PITCH);
			fixedPitch = serializedObject.FindProperty(Properties.FIXED_PITCH);
			pitchRange = serializedObject.FindProperty(Properties.PITCH_RANGE);

			spatialBlend = serializedObject.FindProperty(Properties.SPATIAL_BLEND);
			reverbZoneMix = serializedObject.FindProperty(Properties.REVERB_ZONE_MIX);
			dopplerLevel = serializedObject.FindProperty(Properties.DOPPLER_LEVEL);
			spread = serializedObject.FindProperty(Properties.SPREAD);
			//distanceRange = serializedObject.FindProperty(Properties.DISTANCE_RANGE);
			minDistance = serializedObject.FindProperty(Properties.MIN_DISTANCE);
			maxDistance = serializedObject.FindProperty(Properties.MAX_DISTANCE);
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
			using (new BoxGroupScope(Strings.INFO)) {
				EditorGUILayout.PropertyField(key, Styles.key);

				using (new EditorGUI.DisabledGroupScope(true)) {
					EditorGUILayout.Toggle(Styles.requiresLoading, target.RequiresLoading);
					EditorGUILayout.EnumFlagsField(Styles.loadState, target.LoadState);
				}
			}
		}

		private void GeneralGUI() {
			using (new BoxGroupScope(Strings.GENERAL)) {
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
			using (new BoxGroupScope(Strings.CLIP)) {
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
			using (new BoxGroupScope(Strings.PITCH)) {
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
			using (new BoxGroupScope(Strings.SPATIAL)) {
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

		// MARK: - Constants

		internal static class Properties {
			public const string KEY = "key";

			public const string PREFAB_OVERRIDE = "prefabOverride";
			public const string MIXER_GROUP = "mixerGroup";
			public const string IS_VIRTUAL = "isVirtual";
			public const string ATTACHMENT_MODE = "attachmentMode";
			public const string LOOP = "loop";
			public const string VOLUME = "volume";
			public const string PRIORITY = "priority";

			public const string CLIPS = "clips";
			public const string RANDOM_CLIP = "randomClip";
			public const string CLIP_INDEX = "clipIndex";

			public const string RANDOM_PITCH = "randomPitch";
			public const string FIXED_PITCH = "fixedPitch";
			public const string PITCH_RANGE = "pitchRange";

			public const string SPATIAL_BLEND = "spatialBlend";
			public const string REVERB_ZONE_MIX = "reverbZoneMix";
			public const string DOPPLER_LEVEL = "dopplerLevel";
			public const string SPREAD = "spread";
			public const string DISTANCE_RANGE = "distanceRange";
			public const string MIN_DISTANCE = "minDistance";
			public const string MAX_DISTANCE = "maxDistance";
		}

		internal static class Strings {
			public const string INFO = "Info";
			public const string GENERAL = "General";
			public const string CLIP = "Clip";
			public const string PITCH = "Pitch";
			public const string SPATIAL = "Spatial";

		}

		internal static class Styles {
			public static readonly GUIContent key = new GUIContent("Key", "The effect's key, used to access the audio effect in code.");
			public static readonly GUIContent requiresLoading = new GUIContent("Requires Loading", "Do any of the audio clips in this effect have the \"Load in Background\" flag active?");
			public static readonly GUIContent loadState = new GUIContent("Load State", "The load states of all clips in this effect.");

			public static readonly GUIContent prefabOverride = new GUIContent("Prefab Override", "Use this prefab when playing the effect instead of the default.");
			public static readonly GUIContent mixerGroup = new GUIContent("Mixer Group", "The Audio Mixer Group to output to.  Setting this will override the value in a containing AudioEffectSet.");
			public static readonly GUIContent isVirtual = new GUIContent("Is Virtual", "Should the Audio Source be marked virtual?");
			public static readonly GUIContent attachmentMode = new GUIContent("Attachment Mode", "The attachment mode for the effect.  \"Detached\" does not require a GameObject.");
			public static readonly GUIContent loop = new GUIContent("Loop", "Should the effect loop?");
			public static readonly GUIContent volume = new GUIContent("Volume", "The volume of the effect.");
			public static readonly GUIContent priority = new GUIContent("Priority", "The priority of the effect.  When the system is under heavy load, lower priority effects may be ignored.");

			public static readonly GUIContent clips = new GUIContent("Clips", "The available Audio Clips for this effect.");
			public static readonly GUIContent randomClip = new GUIContent("Random Clip", "Should a random clip be selected from the Clips list?.  This is only available when 2 or more clips are in the list.");
			public static readonly GUIContent clipIndex = new GUIContent("Clip Index", "The index of the clip to be played.  This is only available when Random Clip is off and there are 2 or more clips in the list.");

			public static readonly GUIContent randomPitch = new GUIContent("Random Pitch", "Should a random pitch be used each time the effect is played?  A value of 1 will play the effect as normal.");
			public static readonly GUIContent fixedPitch = new GUIContent("Fixed Pitch", "Used a fixed pitch each time this effect is played.  This is only available when Random Pitch is off.");
			public static readonly GUIContent pitchRange = new GUIContent("Pitch Range", "The range of pitches to use.  This is only available when Random Pitch is on.");
		}
	}
}
#endif