//using System;
//using System.Diagnostics;

//namespace Auxilia.Delegation
//{
//	/// <summary>
//	/// Contains extensions for <see cref="Action"/>.
//	/// </summary>
//	public static class ActionExtensions
//	{
//		/// <summary>
//		/// Invokes the action inside a try/catch block.
//		/// </summary>
//		/// <param name="action">Action to invoke.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static void InvokeSafely(this Action action)
//		{
//			try
//			{
//				action?.Invoke();
//			}
//			catch(Exception exception)
//			{
//				Debug.WriteLine("An error occurred while invoking an action.");
//				logger?.LogError(exception, $"An error occurred while invoking \"{actionName}\".");
//			}
//		}
//		/// <summary>
//		/// Invokes the action inside a try/catch block.
//		/// </summary>
//		/// <typeparam name="T1">Argument 1 type.</typeparam>
//		/// <param name="action">Action to invoke.</param>
//		/// <param name="arg1">Argument 1.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static void InvokeSafely<T>(this Action<T> action, T arg1, ILogger logger, string actionName)
//		{
//			try
//			{
//				action?.Invoke(arg1);
//			}
//			catch (Exception exception)
//			{
//				logger?.LogError(exception, $"An error occurred while invoking \"{actionName}\".");
//			}
//		}
//		/// <summary>
//		/// Invokes the action inside a try/catch block.
//		/// </summary>
//		/// <typeparam name="T1">Argument 1 type.</typeparam>
//		/// <typeparam name="T2">Argument 2 type.</typeparam>
//		/// <param name="action">Action to invoke.</param>
//		/// <param name="arg1">Argument 1.</param>
//		/// <param name="arg2">Argument 2.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static void InvokeSafely<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2, ILogger logger, string actionName)
//		{
//			try
//			{
//				action?.Invoke(arg1, arg2);
//			}
//			catch (Exception exception)
//			{
//				logger?.LogError(exception, $"An error occurred while invoking \"{actionName}\".");
//			}
//		}
//		/// <summary>
//		/// Invokes the action inside a try/catch block.
//		/// </summary>
//		/// <typeparam name="T1">Argument 1 type.</typeparam>
//		/// <typeparam name="T2">Argument 2 type.</typeparam>
//		/// <typeparam name="T3">Argument 3 type.</typeparam>
//		/// <param name="action">Action to invoke.</param>
//		/// <param name="arg1">Argument 1.</param>
//		/// <param name="arg2">Argument 2.</param>
//		/// <param name="arg3">Argument 3.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static void InvokeSafely<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3, ILogger logger, string actionName)
//		{
//			try
//			{
//				action?.Invoke(arg1, arg2, arg3);
//			}
//			catch (Exception exception)
//			{
//				logger?.LogError(exception, $"An error occurred while invoking \"{actionName}\".");
//			}
//		}
//		/// <summary>
//		/// Invokes the action inside a try/catch block.
//		/// </summary>
//		/// <typeparam name="T1">Argument 1 type.</typeparam>
//		/// <typeparam name="T2">Argument 2 type.</typeparam>
//		/// <typeparam name="T3">Argument 3 type.</typeparam>
//		/// <typeparam name="T4">Argument 4 type.</typeparam>
//		/// <param name="action">Action to invoke.</param>
//		/// <param name="arg1">Argument 1.</param>
//		/// <param name="arg2">Argument 2.</param>
//		/// <param name="arg3">Argument 3.</param>
//		/// <param name="arg4">Argument 4.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static void InvokeSafely<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, ILogger logger, string actionName)
//		{
//			try
//			{
//				action?.Invoke(arg1, arg2, arg3, arg4);
//			}
//			catch (Exception exception)
//			{
//				logger?.LogError(exception, $"An error occurred while invoking \"{actionName}\".");
//			}
//		}
//	}
//}