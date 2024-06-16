// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using UnityEngine;

namespace AudioTag {
	public partial class AudioDescriptor {
		[Serializable]
		public struct PoolingData {
			[Min(1)] public int maxInstances;
			[Min(2)] public int defaultCapacity;
			public bool collectionChecks;

			// MARK: - Constants

			public static PoolingData Default => new PoolingData {
				maxInstances = 1,
				defaultCapacity = 10,
				collectionChecks = true
			};
		}
	}
}