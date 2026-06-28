using CalastoneTextFilter.App.Filters;

namespace CalastoneTextFilter.App;

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
        var keptWords = words.Where(word => _filters.All(filter => filter.ShouldKeep(word)));
        return string.Join(" ", keptWords);
    }
}

