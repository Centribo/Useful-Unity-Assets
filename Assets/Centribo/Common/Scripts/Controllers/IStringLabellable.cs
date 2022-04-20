namespace Centribo.Common {
	/// <summary>
	/// Interface for classes that can be labelled using a string
	/// Especially useful for user interface elements.
	/// </summary>
	public interface IStringLabellable {
		public string TextLabel { get; set; }
	}
}