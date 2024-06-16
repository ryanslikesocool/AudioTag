// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag {
	public sealed partial class RuntimeProjectSettings : ScriptableObject {
		public static RuntimeProjectSettings Shared { get; private set; }

		// MARK: - Fields

		[SerializeField, Get]
		private InstanceSettings instance = new InstanceSettings {
			instanceHideFlags = HideFlags.HideAndDontSave,
		};

		[SerializeField, Get]
		private PoolSettings pool = new PoolSettings {
			collectionChecks = true,
			defaultCapacity = 100,
			maxSize = 10
		};

		// MARK: - Lifecyle

		private void OnEnable() {
			Shared = this;
		}
	}
}
