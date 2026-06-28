using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextFilter.App.Filters
{
    internal interface IWordFilter
    {
        public bool ShouldKeep(ReadOnlySpan<char> word);
    }
}
