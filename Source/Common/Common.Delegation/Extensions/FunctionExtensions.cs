//using System;
//using Microsoft.Extensions.Logging;

//namespace Auxilia.Delegation
//{
//	public static class FunctionExtensions
//	{
//		/// <summary>
//		/// Invokes the fuction inside a try/catch block.
//		/// Returns default value when an error occurs.
//		/// </summary>
//		/// <typeparam name="TResult">Return type.</typeparam>
//		/// <param name="function">Function to invoke.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static TResult InvokeSafely<TResult>(this Func<TResult> function, ILogger logger, string functionName)
//		{
//			try
//			{
//				return function == null 
//					? default 
//					: function.Invoke();
//			}
//			catch (Exception exception)
//			{
//				logger?.LogError(exception, $"An error occurred while invoking \"{functionName}\".");
//				return default;
//			}
//		}
//		/// <summary>
//		/// Invokes the fuction inside a try/catch block.
//		/// Returns default value when an error occurs.
//		/// </summary>
//		/// <typeparam name="TResult">Return type.</typeparam>
//		/// <typeparam name="T1">Argument 1 type.</typeparam>
//		/// <param name="function">Function to invoke.</param>
//		/// <param name="arg1">Argument 1.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static TResult InvokeSafely<TResult, T1>(this Func<T1, TResult> function, T1 arg1, ILogger logger, string functionName)
//		{
//			try
//			{
//				return function == null 
//					? default 
//					: function.Invoke(arg1);
//			}
//			catch (Exception exception)
//			{
//				logger?.LogError(exception, $"An error occurred while invoking \"{functionName}\".");
//				return default;
//			}
//		}
//		/// <summary>
//		/// Invokes the fuction inside a try/catch block.
//		/// Returns default value when an error occurs.
//		/// </summary>
//		/// <typeparam name="TResult">Return type.</typeparam>
//		/// <typeparam name="T1">Argument 1 type.</typeparam>
//		/// <typeparam name="T2">Argument 2 type.</typeparam>
//		/// <param name="function">Function to invoke.</param>
//		/// <param name="arg1">Argument 1.</param>
//		/// <param name="arg2">Argument 2.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static TResult InvokeSafely<TResult, T1, T2>(this Func<T1, T2, TResult> function, T1 arg1, T2 arg2, ILogger logger, string functionName)
//		{
//			try
//			{
//				return function == null 
//					? default 
//					: function.Invoke(arg1, arg2);
//			}
//			catch (Exception exception)
//			{
//				logger?.LogError(exception, $"An error occurred while invoking \"{functionName}\".");
//				return default;
//			}
//		}
//		/// <summary>
//		/// Invokes the fuction inside a try/catch block.
//		/// Returns default value when an error occurs.
//		/// </summary>
//		/// <typeparam name="TResult">Return type.</typeparam>
//		/// <typeparam name="T1">Argument 1 type.</typeparam>
//		/// <typeparam name="T2">Argument 2 type.</typeparam>
//		/// <typeparam name="T3">Argument 3 type.</typeparam>
//		/// <param name="function">Function to invoke.</param>
//		/// <param name="arg1">Argument 1.</param>
//		/// <param name="arg2">Argument 2.</param>
//		/// <param name="arg3">Argument 3.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static TResult InvokeSafely<TResult, T1, T2, T3>(this Func<T1, T2, T3, TResult> function, T1 arg1, T2 arg2, T3 arg3, ILogger logger, string functionName)
//		{
//			try
//			{
//				return function == null 
//					? default 
//					: function.Invoke(arg1, arg2, arg3);
//			}
//			catch (Exception exception)
//			{
//				logger?.LogError(exception, $"An error occurred while invoking \"{functionName}\".");
//				return default;
//			}
//		}
//	}
//}