namespace CalastoneTextFilter.Core.FilterPipeline.Filters;

/// <summary>
/// A word filter implementation that filters out words where 'middle characters' are vowels. For odd-length words, the middle character is checked. For even-length words, both middle characters are checked. If any of the middle characters are vowels (a, e, i, o, u), the word is filtered out.
/// </summary>
public class MiddleVowelFilter : IWordFilter
{
    /// <summary>
    /// Check if the middle character(s) of the word are vowels. If they are, the word is filtered out (returns false). If not, the word is kept (returns true).
    /// </summary>
    /// <param name="word">The word to check</param>
    /// <returns>False if the middle character(s) are vowels, true otherwise</returns>
    public bool ShouldKeep(ReadOnlySpan<char> word)
    {
        if (word.IsEmpty) return true;
        int mid = word.Length / 2;

        if (word.Length % 2 == 1) // Odd length
        {
            return !IsVowel(word[mid]);
        }
        else // Even length
        {
            return !IsVowel(word[mid - 1]) && !IsVowel(word[mid]);
        }
    }

    private static bool IsVowel(char c)
    {
        return "aeiou".Contains(c, StringComparison.OrdinalIgnoreCase);
    }
}
