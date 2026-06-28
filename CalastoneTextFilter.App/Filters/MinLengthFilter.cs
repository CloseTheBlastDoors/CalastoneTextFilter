using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextFilter.App.Filters
{
    public class MinLengthFilter(int length) : IWordFilter
    {
        public bool ShouldKeep(ReadOnlySpan<char> word)
        {
            return word.Length >= length;
        }
    }
}
