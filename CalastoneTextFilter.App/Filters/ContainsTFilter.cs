using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextFilter.App.Filters
{
    public class ContainsTFilter : IWordFilter
    {
        public bool ShouldKeep(ReadOnlySpan<char> word)
        {
            return !MemoryExtensions.Contains(word, "t", StringComparison.OrdinalIgnoreCase);
        }
    }
}
