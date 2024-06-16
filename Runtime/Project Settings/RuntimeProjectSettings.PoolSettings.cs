// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;

namespace AudioTag {
	public sealed partial class RuntimeProjectSettings {
		[Serializable]
		public struct PoolSettings {
			public bool collectionChecks;
			public int defaultCapacity;
			public int maxSize;
		}
	}
}