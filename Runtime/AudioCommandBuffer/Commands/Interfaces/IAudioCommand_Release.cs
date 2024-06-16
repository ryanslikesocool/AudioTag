using UnityEngine;

namespace AudioTag {
	/// <summary>
	/// An audio command that signals that the instance is released.
	/// </summary>
	/// <remarks>
	/// No commands should occur after a release command.
	/// </remarks>
	public interface IAudioCommand_Release : IAudioCommand { }
}