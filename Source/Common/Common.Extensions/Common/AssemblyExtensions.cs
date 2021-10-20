using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Auxilia.Extensions
{
	public static class AssemblyExtensions
	{
		public static IEnumerable<Type> GetTypesThatImplement<T>(this Assembly assembly)
		{
			Type baseType = typeof(T);
			return assembly.GetTypes().Where(baseType.IsAssignableFrom);
		}

		public static IEnumerable<Type> GetTypesWithAttribute<T>(this Assembly assembly) where T : Attribute
		{
			return assembly.GetTypes().Where(t => t.GetCustomAttribute<T>() != null);
		}
	}
}
