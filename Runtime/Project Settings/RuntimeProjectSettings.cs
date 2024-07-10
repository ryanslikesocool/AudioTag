// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag {
	public sealed partial class RuntimeProjectSettings : _AudioProjectSettings {
		public static RuntimeProjectSettings Shared { get; private set; }

		// MARK: - Fields

		[SerializeField, Get] private AudioCommandDescriptorList resetCommandBuffer;

		[SerializeField, Get]
		private PoolSettings pool = new PoolSettings {
			collectionChecks = true,
			defaultCapacity = 100,
			maxSize = 10,
		};

		// MARK: - Lifecyle

		private void OnEnable() {
			Shared = this;
		}
	}
}
