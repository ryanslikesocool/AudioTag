// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag {
	public sealed partial class RuntimeProjectSettings : _AudioProjectSettings {
		public static RuntimeProjectSettings Shared { get; private set; }

		// MARK: - Fields

		[SerializeField] private AudioCommandDescriptorList resetCommandBuffer;

		[SerializeField]
		private PoolSettings pool = new PoolSettings {
			collectionChecks = true,
			defaultCapacity = 100,
			maxSize = 10,
		};

		// MARK: - Properties

		public AudioCommandDescriptorList ResetCommandBuffer => resetCommandBuffer;
		public PoolSettings Pool => pool;

		// MARK: - Lifecyle

		private void OnEnable() {
			Shared = this;
		}
	}
}
