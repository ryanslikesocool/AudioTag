using System;

namespace AudioTag {
	[AttributeUsage(
		AttributeTargets.Class,
		AllowMultiple = false,
		Inherited = false
	)]
	public sealed class AudioCommandDescriptorAttribute : Attribute {
		/// <summary>
		/// The display name for this type shown in the menu.
		/// </summary>
		public readonly string menuName;

		/// <summary>
		/// The order this item appears in the menu.
		/// </summary>
		public int order { get; set; }

		public const int SECTION_LENGTH = 100;

		public AudioCommandDescriptorAttribute(string menuName) {
			this.menuName = menuName;
		}
	}
}