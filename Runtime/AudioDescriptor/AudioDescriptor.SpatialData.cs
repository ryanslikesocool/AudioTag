// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using Foundation;
using UnityEngine;

namespace AudioTag {
	public partial class AudioDescriptor {
		[Serializable]
		public struct SpatialData {
			[Range(0, 1)] public float blend;
			[Range(0, 1.1f)] public float reverbZoneMix;
			[Range(0, 5)] public float dopplerLevel;
			[Range(0, 360)] public float spread;
			public ClosedRange<float> range;

			// MARK: - Constants

			public static SpatialData Default => new SpatialData {
				blend = 0,
				reverbZoneMix = 1,
				dopplerLevel = 1,
				spread = 0,
				range = new ClosedRange<float>(1, 500)
			};
		}
	}
}