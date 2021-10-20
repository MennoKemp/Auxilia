using Auxilia.Extensions;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static void AddTransient(this IServiceCollection services, IEnumerable<Type> types)
		{
			types.Execute(t => services.AddTransient(t));
		}
	}
}
