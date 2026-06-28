using CalastoneTextFilter.App.FilterPipeline.Filters;

namespace CalastoneTextFilter.App.FilterPipeline;

public class FilterPipeline(IEnumerable<IWordFilter> filters)
{
    private readonly IWordFilter[] _filters = [.. filters];

    public string Apply(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            //TO-DO: Should at least raise a warning here that there was nothing to process...
            return string.Empty;
        }

        var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var keptWords = words.Where(word => _filters.All(filter => filter.ShouldKeep(StripNonLetterOrDigit(word))));
        return string.Join(" ", keptWords);
    }

    private static string StripNonLetterOrDigit(string word)
    {
        string strippedWord = new([.. word.Where(char.IsLetterOrDigit)]);
        return strippedWord;
    }
}

