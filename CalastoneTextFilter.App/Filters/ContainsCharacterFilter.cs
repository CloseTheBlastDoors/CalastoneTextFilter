using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextFilter.App.Filters
{
    public class ContainsCharacterFilter(char character) : IWordFilter
    {
        public bool ShouldKeep(ReadOnlySpan<char> word)
        {
            return !MemoryExtensions.Contains(word, character.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
