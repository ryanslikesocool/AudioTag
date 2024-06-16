// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Foundation.Editors;

namespace AudioTag.Editors {
	[CustomEditor(typeof(AudioDescriptor))]
	[CanEditMultipleObjects]
	internal class AudioDescriptorEditor : UnityEditor.Editor {
		private new AudioDescriptor target;

		private SerializedProperty prefabOverride;
		private SerializedProperty mixerGroup;

		private SerializedProperty loop;
		private SerializedProperty volume;
		private SerializedProperty priority;

		private SerializedProperty pooling_maxInstances;
		private SerializedProperty pooling_defaultCapacity;
		private SerializedProperty pooling_collectionChecks;

		private SerializedProperty clips_backing;
		private SerializedProperty clips_random;
		private SerializedProperty clips_index;

		private SerializedProperty pitch_constant;
		private SerializedProperty pitch_random;
		private SerializedProperty pitch_range;

		private SerializedProperty spatial_blend;
		private SerializedProperty spatial_reverbZoneMix;
		private SerializedProperty spatial_dopplerLevel;
		private SerializedProperty spatial_spread;
		private SerializedProperty spatial_range;

		public void OnEnable() {
			target = (AudioDescriptor)base.target;

			prefabOverride = serializedObject.FindProperty(Properties.PREFAB_OVERRIDE);
			mixerGroup = serializedObject.FindProperty(Properties.MIXER_GROUP);

			loop = serializedObject.FindProperty(Properties.LOOP);
			volume = serializedObject.FindProperty(Properties.VOLUME);
			priority = serializedObject.FindProperty(Properties.PRIORITY);

			SerializedProperty pooling = serializedObject.FindProperty(Properties.POOLING);
			pooling_maxInstances = pooling.FindPropertyRelative(Properties.Pooling.MAX_INSTANCES);
			pooling_defaultCapacity = pooling.FindPropertyRelative(Properties.Pooling.DEFAULT_CAPACITY);
			pooling_collectionChecks = pooling.FindPropertyRelative(Properties.Pooling.COLLECTION_CHECKS);

			SerializedProperty clips = serializedObject.FindProperty(Properties.CLIPS);
			clips_backing = clips.FindPropertyRelative(Properties.Clips.BACKING);
			clips_random = clips.FindPropertyRelative(Properties.Clips.RANDOM);
			clips_index = clips.FindPropertyRelative(Properties.Clips.INDEX);

			SerializedProperty pitch = serializedObject.FindProperty(Properties.PITCH);
			pitch_constant = pitch.FindPropertyRelative(Properties.Pitch.CONSTANT);
			pitch_random = pitch.FindPropertyRelative(Properties.Pitch.RANDOM);
			pitch_range = pitch.FindPropertyRelative(Properties.Pitch.RANGE);

			SerializedProperty spatial = serializedObject.FindProperty(Properties.SPATIAL);
			spatial_blend = spatial.FindPropertyRelative(Properties.Spatial.BLEND);
			spatial_reverbZoneMix = spatial.FindPropertyRelative(Properties.Spatial.REVERB_ZONE_MIX);
			spatial_dopplerLevel = spatial.FindPropertyRelative(Properties.Spatial.DOPPLER_LEVEL);
			spatial_spread = spatial.FindPropertyRelative(Properties.Spatial.SPREAD);
			spatial_range = spatial.FindPropertyRelative(Properties.Spatial.RANGE);
		}

		public override void OnInspectorGUI() {
			serializedObject.Update();

			InfoGUI();
			GeneralGUI();
			PoolingGUI();
			ClipGUI();
			PitchGUI();
			SpatialGUI();

			serializedObject.ApplyModifiedProperties();
		}

		// MARK: - GUI

		private void InfoGUI() {
			using (new BoxGroupScope(Strings.INFO)) {
				using (new EditorGUI.DisabledGroupScope(true)) {
					EditorGUILayout.Toggle(Styles.requiresLoading, target.clips.RequiresLoading);
					EditorGUILayout.EnumFlagsField(Styles.loadState, target.clips.LoadState);
				}
			}
		}

		private void GeneralGUI() {
			using (new BoxGroupScope(Strings.GENERAL)) {
				EditorGUILayout.PropertyField(prefabOverride, Styles.prefabOverride);
				EditorGUILayout.PropertyField(mixerGroup, Styles.mixerGroup);

				EditorGUILayout.PropertyField(loop, Styles.loop);
				EditorGUILayout.PropertyField(volume, Styles.volume);
				EditorGUILayout.PropertyField(priority, Styles.priority);
			}
		}

		private void PoolingGUI() {
			using (new BoxGroupScope(Strings.POOLING)) {
				EditorGUILayout.PropertyField(pooling_maxInstances, Styles.Pooling.maxInstances);
				using (new EditorGUI.DisabledGroupScope(target.pooling.maxInstances < 2)) {
					EditorGUILayout.PropertyField(pooling_defaultCapacity, Styles.Pooling.defaultCapacity);
					EditorGUILayout.PropertyField(pooling_collectionChecks, Styles.Pooling.collectionChecks);
				}
			}
		}

		private void ClipGUI() {
			using (new BoxGroupScope(Strings.CLIPS)) {
				EditorGUILayout.PropertyField(clips_backing, Styles.Clips.backing);
				using (new EditorGUI.DisabledGroupScope(target.clips.Length < 2)) {
					EditorGUILayout.PropertyField(clips_random, Styles.Clips.random);
				}
				using (new EditorGUI.DisabledGroupScope(target.clips.random || target.clips.Length < 2)) {
					EditorGUILayout.PropertyField(clips_index, Styles.Clips.index);
				}
			}
		}

		private void PitchGUI() {
			using (new BoxGroupScope(Strings.PITCH)) {
				EditorGUILayout.PropertyField(pitch_random, Styles.Pitch.random);
				if (!target.pitch.random) {
					EditorGUILayout.PropertyField(pitch_constant, Styles.Pitch.constant);
				} else {
					EditorGUILayout.PropertyField(pitch_range, Styles.Pitch.range);
				}
			}
		}

		private void SpatialGUI() {
			using (new BoxGroupScope(Strings.SPATIAL)) {
				EditorGUILayout.PropertyField(spatial_blend, Styles.Spatial.blend);
				using (new EditorGUI.DisabledGroupScope(target.spatial.blend <= 0)) {
					EditorGUILayout.PropertyField(spatial_reverbZoneMix, Styles.Spatial.reverbZoneMix);
					EditorGUILayout.PropertyField(spatial_dopplerLevel, Styles.Spatial.dopplerLevel);
					EditorGUILayout.PropertyField(spatial_spread, Styles.Spatial.spread);
					EditorGUILayout.PropertyField(spatial_range, Styles.Spatial.range);
				}
			}
		}

		// MARK: - Constants

		internal static class Properties {
			public const string PREFAB_OVERRIDE = "prefabOverride";
			public const string MIXER_GROUP = "mixerGroup";

			public const string LOOP = "loop";
			public const string VOLUME = "volume";
			public const string PRIORITY = "priority";

			public const string POOLING = "_pooling";
			public static class Pooling {
				public const string MAX_INSTANCES = "maxInstances";
				public const string COLLECTION_CHECKS = "collectionChecks";
				public const string DEFAULT_CAPACITY = "defaultCapacity";
			}

			public const string CLIPS = "_clips";
			public static class Clips {
				public const string BACKING = "backing";
				public const string RANDOM = "random";
				public const string INDEX = "index";
			}

			public const string PITCH = "_pitch";
			public static class Pitch {
				public const string RANDOM = "random";
				public const string CONSTANT = "constant";
				public const string RANGE = "range";
			}

			public const string SPATIAL = "_spatial";
			public static class Spatial {
				public const string BLEND = "blend";
				public const string REVERB_ZONE_MIX = "reverbZoneMix";
				public const string DOPPLER_LEVEL = "dopplerLevel";
				public const string SPREAD = "spread";
				public const string RANGE = "range";
			}
		}

		internal static class Strings {
			public const string INFO = "Info";
			public const string GENERAL = "General";
			public const string POOLING = "Pooling";
			public const string CLIPS = "Clips";
			public const string PITCH = "Pitch";
			public const string SPATIAL = "Spatial";
		}

		internal static class Styles {
			public static readonly GUIContent requiresLoading = new GUIContent("Requires Loading", "Do any of the audio clips in this effect have the \"Load in Background\" flag active?");
			public static readonly GUIContent loadState = new GUIContent("Load State", "The load states of all clips in this effect.");

			public static readonly GUIContent prefabOverride = new GUIContent("Prefab Override", "Use this prefab when playing the effect instead of the default.");
			public static readonly GUIContent mixerGroup = new GUIContent("Mixer Group", "The Audio Mixer Group to output to.  Setting this will override the value in a containing AudioEffectSet.");

			public static readonly GUIContent loop = new GUIContent("Loop", "Should the effect loop?");
			public static readonly GUIContent volume = new GUIContent("Volume", "The volume of the effect.");
			public static readonly GUIContent priority = new GUIContent("Priority", "The priority of the effect.  When the system is under heavy load, lower priority effects may be ignored.");

			public static class Pooling {
				public static readonly GUIContent maxInstances = new GUIContent("Max Instances", "The maximum number of instances allowed in the pool.  If set to 1, the audio source will be marked virtual.");
				public static readonly GUIContent defaultCapacity = new GUIContent("Default Capacity", "The default number of instances to instantiate.");
				public static readonly GUIContent collectionChecks = new GUIContent("Collection Checks", "Enabled collection checks on the pool.");
			}

			public static class Clips {
				public static readonly GUIContent backing = new GUIContent("Clips", "The available Audio Clips for this effect.");
				public static readonly GUIContent random = new GUIContent("Random", "Should a random clip be selected from the Clips list?.  This is only available when 2 or more clips are in the list.");
				public static readonly GUIContent index = new GUIContent("Index", "The index of the clip to be played.  This is only available when Random Clip is off and there are 2 or more clips in the list.");
			}

			public static class Pitch {
				public static readonly GUIContent random = new GUIContent("Random", "Should a random pitch be used each time the effect is played?  A value of 1 will play the effect as normal.");
				public static readonly GUIContent constant = new GUIContent("Constant", "Used a constant each time this effect is played.  This is only available when Random Pitch is off.");
				public static readonly GUIContent range = new GUIContent("Range", "The range of pitches to use.  This is only available when Random Pitch is on.");
			}

			public static class Spatial {
				public static readonly GUIContent blend = new GUIContent("Blend");
				public static readonly GUIContent reverbZoneMix = new GUIContent("Reverb Zone Mix");
				public static readonly GUIContent dopplerLevel = new GUIContent("Doppler Level");
				public static readonly GUIContent spread = new GUIContent("Spread");
				public static readonly GUIContent range = new GUIContent("Range");
			}
		}
	}
}
#endif