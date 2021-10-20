using Auxilia.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Auxilia.Data.Collections
{
	/// <summary>
	/// Wrapper for a list of lists.
	/// </summary>
	/// <typeparam name="T">Type of elements in the list.</typeparam>
	public class JaggedList<T> : List<List<T>>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="JaggedList{T}"/>.
        /// </summary>
        public JaggedList()
        {
        }
        /// <summary>
        /// Initializes a new instance of <see cref="JaggedList{T}"/>.
        /// </summary>
        /// <param name="values">List values.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <see langword="null"/>.</exception>
        public JaggedList(IEnumerable<IEnumerable<T>> values)
            : base(values.ThrowIfNull(nameof(values)).Select(v => v?.ToList()))
        {
        }

        /// <summary>
        /// Makes all lists in the <see cref="JaggedList{T}"/> the same length as the longest list by adding the given default element. 
        /// </summary>
        /// <param name="defaultValue">Default value (optional).</param>
        public void MakeEqualLength(T defaultValue = default)
        {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
            if (this.Any())
            {
                int columnCount = this.Max(r => r.Count);

                this.Where(l => l.Count < columnCount)
                    .Execute(l => l.AddRange(Enumerable.Repeat(defaultValue, columnCount - l.Count)));
            }
#pragma warning restore ExceptionNotDocumented // Exception is not documented
        }
    }
}