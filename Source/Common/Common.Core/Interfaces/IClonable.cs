namespace Auxilia
{
	/// <summary>
	/// Interface to be used for clonable items.
	/// </summary>
	/// <typeparam name="T">Type of elements.</typeparam>
	public interface ICloneable<out T>
	{
		/// <summary>
		/// Clones an object.
		/// </summary>
		/// <returns>Cloned object.</returns>
		T Clone();
	}
}
