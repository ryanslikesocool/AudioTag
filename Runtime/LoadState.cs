// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AudioTag {
	[Flags]
	public enum LoadState : byte {
		None = 0,
		Unloaded = 1 << 0,
		Loading = 1 << 1,
		Loaded = 1 << 2,
		Failed = 1 << 3
	}

	public static partial class Extensions {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static LoadState ToAudioTag(this AudioDataLoadState unity) => (LoadState)(1 << (int)unity);
	}
}