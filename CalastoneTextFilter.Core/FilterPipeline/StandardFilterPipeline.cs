using CalastoneTextFilter.Core.FilterPipeline.Filters;
using Microsoft.Extensions.Logging;

namespace CalastoneTextFilter.Core.FilterPipeline;

/// <summary>
/// A standard implementation of <see cref="IFilterPipeline"/>
/// Suitable for standard throughput and string input sizes where performance is not critical.
/// </summary>
public class StandardFilterPipeline(IEnumerable<IWordFilter> filters, ILogger<StandardFilterPipeline> logger) : IFilterPipeline
{
    private readonly IWordFilter[] _filters = [.. filters];

    public string Apply(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            logger.LogWarning("Input string is null or whitespace. Returning empty string.");
            return string.Empty;
        }

        var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var keptWords = words.Where(word => _filters.All(filter => filter.ShouldKeep(StripNonLetterOrDigit(word))));
        return string.Join(" ", keptWords);
    }

    private static string StripNonLetterOrDigit(string word)
    {
        return new([.. word.Where(char.IsLetterOrDigit)]);
    }
}
