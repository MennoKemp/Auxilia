//using System;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;

//namespace Auxilia.Delegation
//{
//	public static class TaskExtensions
//	{
//        public static async void InvokeSafely(this Task task, ILogger logger = null)
//        {
//            try
//            {
//                await task;
//            }
//            catch (Exception exception)
//            {
//                logger?.LogError(exception, "An error occurred while invoking a task.");
//            }
//        }
//    }
//}
