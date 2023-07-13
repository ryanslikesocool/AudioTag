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
    public struct AudioEffectTag : IEquatable<AudioEffectTag> {
#if ODIN_INSPECTOR_3
        [HideLabel]
#endif
        public string tag;

        public AudioEffectTag(string tag) {
            this.tag = tag;
        }

        public static implicit operator Runtime(AudioEffectTag tag) => new Runtime(tag);

        public bool Equals(AudioEffectTag other) => tag == other.tag;

#if ODIN_INSPECTOR_3
        [InlineProperty]
#endif
        public readonly struct Runtime : IEquatable<Runtime> {
#if ODIN_INSPECTOR_3
            [HideLabel]
#endif
            public readonly int tag;

            public Runtime(int tag) {
                this.tag = tag;
            }

            public Runtime(AudioEffectTag tag) : this(tag.tag.GetHashCode()) { }

            public static readonly Runtime zero = new Runtime(0);

            public bool Equals(AudioEffectTag.Runtime other) => tag == other.tag;

            public static bool operator ==(Runtime lhs, Runtime rhs) => lhs.Equals(rhs);
            public static bool operator !=(Runtime lhs, Runtime rhs) => !lhs.Equals(rhs);

            public override bool Equals(object obj) => obj switch {
                Runtime other => this.Equals(other),
                int tag => this.tag == tag,
                _ => throw new Exception()
            };

            public override int GetHashCode() => tag;
        }
    }
}