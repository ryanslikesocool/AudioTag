// Developed With Love by Ryan Boyer http://ryanjboyer.com <3

using System;
using UnityEngine;

namespace AudioTag {
	[Serializable]
	public struct AudioKey : IEquatable<AudioKey> {
		[SerializeField] private AudioKeyReference _reference;
		[SerializeField] private string _value;

		public readonly string key => _reference.key ?? _value;

		public AudioKey(AudioKeyReference key) {
			_reference = key;
			_value = default;
		}

		public AudioKey(in string key) {
			_reference = default;
			_value = key;
		}

		// MARK: - IEquatable

		public readonly bool Equals(AudioKey other) => key == other.key;

		public static implicit operator string(AudioKey field) => field.key;
		public static implicit operator AudioKey(string field) => new AudioKey { _value = field };

		public static bool operator ==(AudioKey lhs, AudioKey rhs) => lhs.Equals(rhs);
		public static bool operator !=(AudioKey lhs, AudioKey rhs) => !lhs.Equals(rhs);

		// MARK: - Override

		public readonly override bool Equals(object other) => other switch {
			AudioKey _other => this.Equals(_other),
			_ => throw new ArgumentException()
		};

		public readonly override int GetHashCode() => (_reference, _value).GetHashCode();
	}
}
