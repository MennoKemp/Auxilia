using System;
using System.ComponentModel.Design;

namespace Auxilia
{
    ///// <summary>
    ///// This class can be used to indicate a successful or failed result with a clarifying message.
    ///// </summary>
    //public class Result
    //{
    //    /// <summary>
    //    /// Initializes a new instance of a successful <see cref="Result"/>.
    //    /// </summary>
    //    /// <param name="message">Message clarifying the result.</param>
    //    internal Result(string message, bool isLogged)
    //    {
    //        Success = true;
    //        Message = message.ThrowIfNullOrEmpty(nameof(message));
    //        IsLogged = isLogged;
    //    }
    //    /// <summary>
    //    /// Initializes a new instance of a failed <see cref="Result"/>.
    //    /// </summary>
    //    /// <param name="message">Message clarifying the result.</param>
    //    /// <param name="exception">Exception that caused the failure (optional).</param>
    //    internal Result(string message, Exception exception, bool isLogged)
    //    {
    //        Success = false;
    //        Message = message.ThrowIfNullOrEmpty(nameof(message));
    //        Exception = exception;
    //        IsLogged = isLogged;
    //    }

    //    /// <summary>
    //    /// Gets a value indicating if the result is successful.
    //    /// </summary>
    //    /// <message>True if the result is successful; false, otherwise.</message>
    //    public bool Success { get; }
    //    /// <summary>
    //    /// Gets a message clarifying the result.
    //    /// </summary>
    //    public string Message { get; }

    //    /// <summary>
    //    /// Gets the exception that caused a failed result.
    //    /// </summary>
    //    public Exception Exception { get; }

    //    /// <summary>
    //    /// Gets or sets a value indicating if the result has been logged.
    //    /// </summary>
    //    public bool IsLogged { get; set; }

    //    /// <summary>
    //    /// Creates a successful result.
    //    /// </summary>
    //    /// <param name="message">Message clarifying the result.</param>
    //    /// <returns>A successful result.</returns>
    //    public static Result Successful(string message, bool isLogged = false)
    //    {
    //        return new Result(message, isLogged);
    //    }

    //    /// <summary>
    //    /// Creates a failed result.
    //    /// </summary>
    //    /// <param name="message">Message clarifying the result.</param>
    //    /// <param name="exception">Exception that caused the failure (optional).</param>
    //    /// <returns>A failed result.</returns>
    //    public static Result Failed(string message, Exception exception = null, bool isLogged = false)
    //    {
    //        return new Result(message, exception, isLogged);
    //    }

    //    /// <summary>
    //    /// Returns object string including the message and exception if not specified.
    //    /// </summary>
    //    /// <returns>Object string.</returns>
    //    public override string ToString()
    //    {
	   //     return Exception != null
		  //      ? $"Error: {Message}{Environment.NewLine}{Exception}"
		  //      : Success
			 //       ? $"Success: {Message}"
			 //       : $"Fail: {Message}";
    //    }

    //    /// <summary>
    //    /// Returns a value indicating if the result is successful.
    //    /// </summary>
    //    /// <example>
    //    /// This method can be used to check the result from a method and use it in the next line.
    //    /// <code>
    //    /// if(SomeMethod.IsSuccessful(out Result result))
    //    ///     Do something with the (successful) result.
    //    /// </code>
    //    /// </example>
    //    /// <param name="result">An instance of the result.</param>
    //    /// <message>True if the result is successful; false otherwise.</message>
    //    public bool IsSuccessful(out Result result)
    //    {
    //        result = this;
    //        return Success;
    //    }
    //}
}
