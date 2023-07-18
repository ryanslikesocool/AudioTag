#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace AudioTag.Editors {
    [CustomPropertyDrawer(typeof(AudioKey.Runtime))]
    internal sealed class AudioKeyRuntimeDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			SerializedProperty keyProperty = property.FindPropertyRelative("key");

            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Clear indent
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Draw
            EditorGUI.PropertyField(position, keyProperty, GUIContent.none);

            // Restore indent
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
#endif