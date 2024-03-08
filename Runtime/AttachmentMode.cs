// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag {
	public enum AttachmentMode : byte {
		/// <summary>
		/// The audio effect is attached to an <see cref="AudioSource"/>.
		/// </summary>
		Attached,
		/// <summary>
		/// The audio effect is attached to an <see cref="AudioSource"/> and marked virtual.
		/// </summary>
		[InspectorName("Attached (Virtual)")] AttachedVirtual,
		/// <summary>
		/// The audio effect is detached.
		/// </summary>
		/// <remarks>
		/// Audio effects marked as 'Detached' are played using <see cref="AudioSource.PlayClipAtPoint"/>.
		/// </remarks>
		Detached
	}
}