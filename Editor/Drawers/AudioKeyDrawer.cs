// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AudioTag.Editor {
	[CustomPropertyDrawer(typeof(AudioKey))]
	internal sealed class AudioKeyDrawer : PropertyDrawer {
		private const float SPACING = 4;
		private const float PICKER_WIDTH = 16;

		private enum Mode : byte {
			Value,
			Reference
		}

		private Mode mode = (Mode)byte.MaxValue;
		private GUIContent menuContent = null;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			SerializedProperty referenceProperty = property.FindPropertyRelative("_reference");
			SerializedProperty valueProperty = property.FindPropertyRelative("_value");

			if (mode == (Mode)byte.MaxValue) {
				mode = referenceProperty.objectReferenceValue == null ? Mode.Value : Mode.Reference;
			}

			EditorGUI.BeginProperty(position, label, property);

			// Draw label
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			// Clear indent
			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// Calculate rects
			Rect valueRect = new Rect(position.x, position.y, position.width - (PICKER_WIDTH + SPACING), position.height);
			float consumed = valueRect.width + SPACING;
			Rect pickerRect = new Rect(position.x + consumed, position.y, PICKER_WIDTH, position.height);

			// Draw

			SerializedProperty drawnProperty = mode switch {
				Mode.Value => valueProperty,
				Mode.Reference => referenceProperty,
				_ => null
			};
			EditorGUI.PropertyField(valueRect, drawnProperty, GUIContent.none);

			if (menuContent == null) {
				Texture2D icon = EditorGUIUtility.FindTexture("_Menu");
				menuContent = new GUIContent(icon);
			}

			if (EditorGUI.DropdownButton(pickerRect, menuContent, FocusType.Passive, EditorStyles.iconButton)) { // menu
				GenericMenu menu = new GenericMenu();
				menu.AddItem(new GUIContent("Value"), mode == Mode.Value, () => mode = Mode.Value);
				menu.AddItem(new GUIContent("Reference"), mode == Mode.Reference, () => mode = Mode.Reference);
				menu.ShowAsContext();
			}

			// Restore indent
			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty();
		}
	}
}
#endif