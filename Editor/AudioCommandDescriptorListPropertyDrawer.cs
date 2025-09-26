// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.Editor;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AudioTag.Editors {
	//[CustomPropertyDrawer(typeof(List<AudioCommandDescriptor>))] // these don't work for whatever reason
	//[CustomPropertyDrawer(typeof(AudioCommandDescriptor[]))]

	[CustomPropertyDrawer(typeof(AudioCommandDescriptorList))]
	internal sealed class AudioCommandDescriptorListPropertyDrawer : PropertyDrawer {
		private ReorderableList list;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			using EditorGUI.PropertyScope scope = new(position, label, property);

			SerializedProperty backing = property.FindPropertyRelative(nameof(AudioCommandDescriptorList.backing));
			OnListGUI(property.serializedObject, backing, label);
		}

		// MARK: - GUI

		private void OnListGUI(SerializedObject serializedObject, SerializedProperty backing, GUIContent label) {
			list ??= new(
				serializedObject,
				backing,
				draggable: true,
				displayHeader: true,
				displayAddButton: true,
				displayRemoveButton: true
			);

			list.drawElementCallback = OnListElementGUI;
			list.drawHeaderCallback = OnListHeaderGUI;
			list.elementHeightCallback = OnListElementHeight;
			list.onDeleteArrayElementCallback = OnDeleteListElement;
			list.onAddDropdownCallback = OnAddDropdown;

			list.DoLayoutList();

			void OnListElementGUI(Rect rect, int index, bool isActive, bool isFocused) {
				SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
				SerializedObject obj = new(element.objectReferenceValue);

				DoDrawDefaultInspector(obj, rect);
			}

			void OnListHeaderGUI(Rect rect) {
				EditorGUI.LabelField(rect, label);
			}

			float OnListElementHeight(int index) {
				SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
				SerializedObject obj = new(element.objectReferenceValue);

				return GetElementHeight(obj);
			}

			void OnAddDropdown(Rect buttonRect, ReorderableList list) {
				CreateNewCommandMenu(OnSelectNewCommandMenuItem).ShowAsContext();

				void OnSelectNewCommandMenuItem(object value) {
					backing.arraySize += 1;

					backing.GetArrayElementAtIndex(backing.arraySize - 1).objectReferenceValue
						= SubObjectUtility.CreateSubObject(serializedObject.targetObject, (Type)value, value.ToString());

					backing.serializedObject.ApplyModifiedProperties();
				}
			}

			void OnDeleteListElement(ReorderableList list, int index) {
				SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
				UnityEngine.Object elementObject = element.objectReferenceValue;

				SubObjectUtility.DestroySubObject(elementObject);
				backing.DeleteArrayElementAtIndex(index);
			}
		}

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
					AudioCommandDescriptorAttribute attribute = type.GetCustomAttributes(typeof(AudioCommandDescriptorAttribute), inherit: false).Cast<AudioCommandDescriptorAttribute>().First();
					return (type, attribute);
				})
				.OrderBy(pair => pair.attribute.order);

		private static float GetElementHeight(SerializedObject obj) {
			float result = 0;

			SerializedProperty iterator = obj.GetIterator();
			bool enterChildren = true;
			while (iterator.NextVisible(enterChildren)) {
				result += EditorGUI.GetPropertyHeight(iterator, true);
				enterChildren = false;
			}

			return result;
		}

		// based on `bool UnityEditor.Editor.DoDrawDefaultInspector(SerializedObject) { }`
		private static bool DoDrawDefaultInspector(SerializedObject obj, Rect rect) {
			rect.height = EditorGUIUtility.singleLineHeight;

			EditorGUI.BeginChangeCheck();
			obj.UpdateIfRequiredOrScript();
			SerializedProperty iterator = obj.GetIterator();
			bool enterChildren = true;
			while (iterator.NextVisible(enterChildren)) {
				using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath)) {
					EditorGUI.PropertyField(rect, iterator, true);
				}
				rect.y += EditorGUIUtility.singleLineHeight;

				enterChildren = false;
			}

			obj.ApplyModifiedProperties();
			return EditorGUI.EndChangeCheck();
		}

		// MARK: - Constants

		private const char MENU_SEPARATOR = '/';
	}
}