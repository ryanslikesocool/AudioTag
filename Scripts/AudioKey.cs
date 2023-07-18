using System;
#if ODIN_INSPECTOR_3
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace AudioTag {
#if ODIN_INSPECTOR_3
	[InlineProperty]
#endif
	[Serializable]
	public struct AudioKey : IEquatable<AudioKey> {
#if ODIN_INSPECTOR_3
		[HideLabel]
#endif
		public string key;

		public AudioKey(string key) {
			this.key = key;
		}

		public static implicit operator Runtime(AudioKey tag) => new Runtime(tag);

		public bool Equals(AudioKey other) => key == other.key;

#if ODIN_INSPECTOR_3
		[InlineProperty]
#endif
		public readonly struct Runtime : IEquatable<Runtime> {
#if ODIN_INSPECTOR_3
			[HideLabel]
#endif
			public readonly int key;

			public Runtime(int key) {
				this.key = key;
			}

			public Runtime(AudioKey key) : this(key.key.GetHashCode()) { }

			public static readonly Runtime zero = new Runtime(0);

			public bool Equals(AudioKey.Runtime other) => key == other.key;

			public static bool operator ==(Runtime lhs, Runtime rhs) => lhs.Equals(rhs);
			public static bool operator !=(Runtime lhs, Runtime rhs) => !lhs.Equals(rhs);

			public override bool Equals(object obj) => obj switch {
				Runtime other => this.Equals(other),
				int key => this.key == key,
				_ => throw new Exception()
			};

			public override int GetHashCode() => key;
		}
	}
}