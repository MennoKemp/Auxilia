using System.Collections.Generic;
using System.Linq;

namespace Auxilia.Utilities
{
	public static class MathUtils
	{
		public static int FindLowestPositive(IEnumerable<int> takenNumbers, bool includeZero = true)
		{
			if (takenNumbers == null)
				return includeZero ? 0 : 1;

			SortedSet<int> numbers = new SortedSet<int>(takenNumbers);
			
			if(!numbers.Any())
				return includeZero ? 0 : 1;

			for (int i = includeZero ? 0 : 1; i < numbers.Last(); i++)
			{
				if (!numbers.Contains(i))
					return i;
			}

			return numbers.Last() + 1;
		}
	}
}
