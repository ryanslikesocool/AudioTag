// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

#if UNITY_EDITOR
using UnityEditor;

namespace AudioTag.Editors {
	public sealed class GroupBox : EditorGUILayout.VerticalScope {
		public GroupBox(string label = null) : base(GROUP_BOX) {
			if (!string.IsNullOrEmpty(label)) {
				EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
			}
			EditorGUI.indentLevel++;
		}

		protected override void CloseScope() {
			EditorGUI.indentLevel--;
			base.CloseScope();
		}

		private const string GROUP_BOX = "GroupBox";
	}
}
#endif