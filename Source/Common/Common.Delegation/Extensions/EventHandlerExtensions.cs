//using System;
//using Microsoft.Extensions.Logging;

//namespace Auxilia.Delegation
//{
//	/// <summary>
//	/// Contains extensions for <see cref="EventHandler"/>.
//	/// </summary>
//	public static class EventHandlerExtensions
//	{
//		/// <summary>
//		/// Invokes the event handler inside a try/catch block.
//		/// </summary>
//		/// <param name="eventHandler">Handler to invoke.</param>
//		/// <param name="sender">Invoker of the event.</param>
//		/// <param name="e">Event args.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static void InvokeSafely(this EventHandler eventHandler, object sender, EventArgs e, ILogger logger, string eventName)
//		{
//			try
//			{
//				eventHandler?.Invoke(sender, e);
//			}
//			catch (Exception exception)
//			{
//				logger?.LogError(exception, $"An error occurred while handling \"{eventName}\".");
//			}
//		}
//		/// <summary>
//		/// Invokes the event handler inside a try/catch block.
//		/// </summary>
//		/// <typeparam name="T">Argument type.</typeparam>
//		/// <param name="eventHandler">Handler to invoke.</param>
//		/// <param name="sender">Invoker of the event.</param>
//		/// <param name="e">Event args.</param>
//		/// <param name="handleException">Handler for errors.</param>
//		public static void InvokeSafely<T>(this EventHandler<T> eventHandler, object sender, T e, ILogger logger, string eventName)
//		{
//			try
//			{
//				eventHandler?.Invoke(sender, e);
//			}
//			catch(Exception exception)
//			{
//				logger?.LogError(exception, $"An error occurred while handling \"{eventName}\".");
//			}
//		}
//	}
//}