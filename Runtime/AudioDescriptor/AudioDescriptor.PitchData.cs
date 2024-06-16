// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using Foundation;
using UnityEngine;

namespace AudioTag {
	public partial class AudioDescriptor {
		[Serializable]
		public struct PitchData {
			public bool random;
			[Range(-3, 3)] public float constant;
			public ClosedRange<float> range;

			// MARK: - Constants

			public static PitchData Default => new PitchData {
				random = false,
				constant = 1,
				range = new ClosedRange<float>(1, 1)
			};

			// MARK: -

			public readonly float GetValue() {
				if (random) {
					return UnityEngine.Random.Range(range.x, range.y);
				} else {
					return constant;
				}
			}
		}
	}
}