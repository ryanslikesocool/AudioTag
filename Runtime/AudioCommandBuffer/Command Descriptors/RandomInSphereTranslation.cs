using UnityEngine;

namespace AudioTag {
	public static partial class AudioCommand {
		public static partial class Descriptor {
			public sealed class RandomInSphereTranslation : AudioCommandDescriptor {
				public Space space = Space.Self;
				public Vector3 center = Vector3.zero;
				[Min(float.Epsilon)] public float radius = 1.0f;

				// MARK: -

				public override IAudioCommand Resolve()
					=> AudioCommand.Translation.RandomInSphere(space, center, radius);
			}
		}
	}
}