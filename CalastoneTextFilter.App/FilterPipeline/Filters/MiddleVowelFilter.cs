namespace CalastoneTextFilter.App.FilterPipeline.Filters;

public class MiddleVowelFilter : IWordFilter
{
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

    private bool IsVowel(char c)
    {
        return "aeiou".Contains(c, StringComparison.OrdinalIgnoreCase);
    }
}

