using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace AudioTag {
	public static partial class Exetensions {
		[MethodImpl(AggressiveInlining)]
		public static void Execute(this IEnumerable<IAudioCommand> collection, ref AudioCommandBuffer.Context context) {
			foreach (IAudioCommand element in collection) {
				element.Execute(ref context);
			}
		}

		[MethodImpl(AggressiveInlining)]
		public static IEnumerable<IAudioCommand> Resolve(this IEnumerable<AudioCommandDescriptor> collection)
			=> collection.Select(element => element.Resolve());

		[MethodImpl(AggressiveInlining)]
		public static void Execute(this AudioSource source, in AudioCommandBuffer commandBuffer)
			=> commandBuffer.Execute(source);
	}
}