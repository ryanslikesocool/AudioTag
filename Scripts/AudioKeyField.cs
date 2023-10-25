using System;
using UnityEngine;

namespace AudioTag {
	[Serializable]
	public struct AudioKeyField : IEquatable<AudioKeyField> {
		[SerializeField] private AudioKeyReference _reference;
		[SerializeField] private AudioKey _value;

		public readonly AudioKey.Runtime key => _reference?.key ?? _value;

		public readonly bool Equals(AudioKeyField other) => key == other.key;

		public static implicit operator AudioKey.Runtime(AudioKeyField field) => field.key;

		public static implicit operator AudioKeyField(string key) => new AudioKeyField { _value = new AudioKey(key) };

		public static bool operator ==(AudioKeyField lhs, AudioKeyField rhs) => lhs.Equals(rhs);
		public static bool operator !=(AudioKeyField lhs, AudioKeyField rhs) => !lhs.Equals(rhs);

		public override bool Equals(object other) => other switch {
			AudioKeyField _other => this.Equals(_other),
			//AudioKey _other => this.Equals(_other),
			_ => throw new ArgumentException()
		};

		public override int GetHashCode() => (_reference, _value).GetHashCode();
	}
}