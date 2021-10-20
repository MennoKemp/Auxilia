using System;

namespace Auxilia
{
	/// <summary>
	/// Exception thrown when an error occurs while invoking a delegate.
	/// </summary>
	public class InvocationException : Exception
	{
		/// <summary>
		/// Initializes a new instance of <see cref="InvocationException"/>.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public InvocationException(string message)
			: base(message)
		{
		}
		/// <summary>
		/// Initializes a new instance of <see cref="InvocationException"/>.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception.</param>
		public InvocationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}