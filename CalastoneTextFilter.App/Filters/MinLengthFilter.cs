using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextFilter.App.Filters
{
    public class MinLengthFilter
    {
        public bool ShouldKeep(string word)
        {
            return word.Length >= 3;
        }
    }
}
