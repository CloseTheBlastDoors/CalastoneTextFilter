using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextFilter.App.Filters
{
    public class MiddleVowelFilter : IWordFilter
    {
        public bool ShouldKeep(ReadOnlySpan<char> word)
        {
            if (word.IsEmpty) return true;

            string wordAsString = word.ToString();
            int mid = wordAsString.Length / 2;

            if (wordAsString.Length % 2 == 1) // Odd length
            {
                return !IsVowel(wordAsString[mid]);
            }
            else // Even length
            {
                return !IsVowel(wordAsString[mid - 1]) && !IsVowel(wordAsString[mid]);
            }
        }

        private bool IsVowel(char c)
        {
            return "aeiou".Contains(c, StringComparison.OrdinalIgnoreCase);
        }
    }
}
