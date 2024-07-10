// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Foundation.Editors;
using System.Collections.Generic;
using Sirenix.Utilities;
using Foundation;

namespace AudioTag.Editors {
	[CustomPropertyDrawer(typeof(AudioCommandDescriptorList))]
	internal sealed class AudioCommandDescriptorListPropertyDrawer : PropertyDrawer {
		public bool useFoldout = true;

		//public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		//	//return 1000;
		//}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			UnityEngine.Object rootObject = property.serializedObject.targetObject;
			OnGUI(position, rootObject, property, label);
		}

		public void OnGUI(Rect position, UnityEngine.Object rootObject, SerializedProperty property, GUIContent label) {
			using (var scope = new EditorGUI.PropertyScope(position, label, property)) {
				label = scope.content;
				SerializedProperty backing = property.FindPropertyRelative("backing");

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel)) {
					//if (useFoldout) {
					//	property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, GUIContent.none);
					//	if (property.isExpanded) {
					//		OnListGUI(rootObject, property, backing);
					//	}
					//} else {
					OnListGUI(rootObject, property, backing);
					//}
				}
			}
		}

		// MARK: - GUI

		private void OnListGUI(UnityEngine.Object rootObject, SerializedProperty property, SerializedProperty backing) {
			using (new FoundationEditorGUI.BoxGroupScope(property.displayName)) {
				if (backing.hasMultipleDifferentValues) {
					EditorGUILayout.HelpBox(Style.MultipleValues, MessageType.Warning);
				} else {
					for (int i = 0; i < backing.arraySize; i++) {
						using (new EditorGUILayout.HorizontalScope()) {
							UnityEngine.Object commandObject = backing.GetArrayElementAtIndex(i).objectReferenceValue;

							using (new EditorGUILayout.VerticalScope()) {
								Editor.CreateEditor(commandObject)
									.DrawDefaultInspector();
							}

							if (GUILayout.Button(Style.MinusIcon, Style.MinusButtonLayout)) {
								SubObjectUtility.DestroySubObject(commandObject);
								backing.DeleteArrayElementAtIndex(i);
								continue;
							}
						}

						FoundationEditorGUI.HorizontalLine();
					}

					using (new EditorGUILayout.HorizontalScope()) {
						EditorGUILayout.Space(0, true);
						if (EditorGUILayout.DropdownButton(Style.PlusIcon, FocusType.Keyboard, Style.PlusButtonLayout)) {
							CreateNewCommandMenu(OnSelectNewCommandMenuItem).ShowAsContext();
						}
					}
				}
			}

			void OnSelectNewCommandMenuItem(object value) {
				backing.arraySize += 1;

				backing.GetArrayElementAtIndex(backing.arraySize - 1).objectReferenceValue
					= SubObjectUtility.CreateSubObject(rootObject, (Type)value, value.ToString());

				backing.serializedObject.ApplyModifiedProperties();
			}
		}

		// MARK: - Utility

		private static GenericMenu CreateNewCommandMenu(GenericMenu.MenuFunction2 func) {
			Dictionary<string, int> orders = new Dictionary<string, int>();
			GenericMenu menu = new GenericMenu();

			foreach ((Type type, AudioCommandDescriptorAttribute attribute) in GetCommandTypes()) {
				int removeStart = attribute.menuName.LastIndexOf(MENU_SEPARATOR) + 1;
				int removeLength = attribute.menuName.Length - removeStart;
				string group = attribute.menuName.Remove(removeStart, removeLength);

				orders.TryAdd(group, int.MinValue);

				if (orders[group] > int.MinValue && Mathf.Abs(attribute.order - orders[group]) > AudioCommandDescriptorAttribute.SECTION_LENGTH) {
					menu.AddSeparator(group);
				}

				menu.AddItem(new GUIContent(attribute.menuName), on: false, func: func, type);

				orders[group] = attribute.order;
			}

			return menu;
		}

		private static IEnumerable<(Type, AudioCommandDescriptorAttribute)> GetCommandTypes()
			=> TypeCache.GetTypesWithAttribute<AudioCommandDescriptorAttribute>()
				.Select(type => {
					AudioCommandDescriptorAttribute attribute = type.GetAttributes<AudioCommandDescriptorAttribute>(inherit: false).First();
					return (type, attribute);
				})
				.OrderBy(pair => pair.attribute.order);

		// MARK: - Constants

		private const char MENU_SEPARATOR = '/';

		private static class Style {
			internal static readonly GUIContent MinusIcon = new GUIContent(EditorGUIUtility.IconContent(Icon.MINUS));
			internal static readonly GUIContent PlusIcon = new GUIContent(EditorGUIUtility.IconContent(Icon.PLUS));

			internal const string MultipleValues = "Cannot display command lists with different values.";

			internal static readonly GUILayoutOption[] MinusButtonLayout = new GUILayoutOption[2] {
				GUILayout.Width(30),
				GUILayout.Height(18)
			};

			internal static readonly GUILayoutOption[] PlusButtonLayout = new GUILayoutOption[2] {
				GUILayout.Width(40),
				GUILayout.Height(18)
			};

			internal static class Icon {
				internal const string MINUS = "Toolbar Minus";
				internal const string PLUS = "Toolbar Plus";
			}
		}
	}
}