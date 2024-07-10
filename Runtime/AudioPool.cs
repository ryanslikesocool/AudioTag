using System.Collections.Generic;
using Foundation;
using UnityEngine;
using UnityEngine.Pool;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace AudioTag {
	[Singleton]
	public sealed partial class AudioPool {
		private AudioSource prefab;

		private readonly HashSet<AudioSource> activeInstances;
		private readonly ObjectPool<AudioSource> pool;

		private RuntimeProjectSettings ProjectSettings => RuntimeProjectSettings.Shared;

		// MARK: - Lifecycle

		private AudioPool() {
			activeInstances = new HashSet<AudioSource>();
			pool = new ObjectPool<AudioSource>(
				createFunc: OnPoolCreate,
				actionOnGet: OnPoolGet,
				actionOnRelease: OnPoolRelease,
				actionOnDestroy: OnPoolDestroy,
				collectionCheck: ProjectSettings.Pool.collectionChecks,
				defaultCapacity: ProjectSettings.Pool.defaultCapacity,
				maxSize: ProjectSettings.Pool.maxSize
			);
		}

		// MARK: -

		private void ValidatePrefab() {
			if (prefab == null) {
				GameObject obj = new GameObject("AudioSource");
				prefab = obj.AddComponent<AudioSource>();
				obj.hideFlags = HideFlags.HideAndDontSave;
			}
		}

		// MARK: - Pool

		private AudioSource OnPoolCreate() {
			ValidatePrefab();

			AudioSource instance = GameObject.Instantiate(prefab);
			return instance;
		}

		private void OnPoolGet(AudioSource instance) {
			instance.Execute(ProjectSettings.ResetCommandBuffer);

			instance.gameObject.SetActive(true);

			activeInstances.Add(instance);
		}

		private void OnPoolRelease(AudioSource instance) {
			activeInstances.Remove(instance);

			instance.gameObject.SetActive(false);
		}

		private void OnPoolDestroy(AudioSource instance) {
			instance.gameObject.DestroySafe();
		}

		// MARK: - Access

		/// <summary>
		/// Retrieve an instance from the pool.
		/// </summary>
		/// <remarks>
		/// The caller is responsible for releasing the object back to the pool.
		/// </remarks>
		[MethodImpl(AggressiveInlining)]
		public AudioSource Get()
			=> pool.Get();

		/// <summary>
		/// Release an instance back to the pool
		/// </summary>
		/// <param name="instance">The instance to release back to the pool.</param>
		[MethodImpl(AggressiveInlining)]
		public void Release(AudioSource instance)
			=> pool.Release(instance);

		/// <summary>
		/// Execute a command buffer with an instance retrieved from the pool.
		/// </summary>
		/// <param name="commandBuffer">The command buffer to execute.</param>
		[MethodImpl(AggressiveInlining)]
		public void Execute(in AudioCommandBuffer commandBuffer) {
			AudioSource instance = Get();
			commandBuffer.Execute(instance);
		}
	}
}