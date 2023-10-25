using System;

namespace AudioTag {
	[Serializable]
	public struct AudioKey : IEquatable<AudioKey> {
		public string key;

		public AudioKey(string key) {
			this.key = key;
		}

		public static implicit operator Runtime(AudioKey key) => new Runtime(key);
		public static implicit operator AudioKey(string key) => new AudioKey(key);

		public readonly bool Equals(AudioKey other) => key == other.key;

		public readonly struct Runtime : IEquatable<Runtime> {
			public readonly int key;

			public Runtime(int key) {
				this.key = key;
			}

			public Runtime(AudioKey key) : this(key.key.GetHashCode()) { }

			public static readonly Runtime zero = new Runtime(0);

			public bool Equals(Runtime other) => key == other.key;

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