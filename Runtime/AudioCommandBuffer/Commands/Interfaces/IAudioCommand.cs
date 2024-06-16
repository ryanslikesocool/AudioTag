// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using Foundation;

namespace AudioTag {
	/// <summary>
	/// A command that can be executed by an <see cref="AudioCommandBuffer"/>
	/// </summary>
	/// <seealso cref="AudioCommandBuffer"/>
	public interface IAudioCommand : ICommandBufferCommand<AudioCommandBuffer.Context> { }
}