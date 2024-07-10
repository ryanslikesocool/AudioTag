// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using UnityEngine;

namespace AudioTag.AudioCommand.Descriptor {
	[AudioCommandDescriptor(
		"Spatial/Translation (Random on Sphere)",
		order = AudioCommandDescriptorAttribute.SECTION_LENGTH * 12 + 3
	)]
	public sealed class RandomOnSphereTranslation : AudioCommandDescriptor {
		public Space space = Space.Self;
		public Vector3 center = Vector3.zero;
		[Min(float.Epsilon)] public float radius = 1.0f;

		// MARK: -

		public override IAudioCommand Resolve()
			=> AudioCommand.Translation.RandomOnSphere(space, center, radius);
	}
}
