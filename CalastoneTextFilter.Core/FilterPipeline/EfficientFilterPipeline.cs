using CalastoneTextFilter.Core.FilterPipeline.Filters;
using Microsoft.Extensions.Logging;
using System.Text;

namespace CalastoneTextFilter.Core.FilterPipeline;

/// <summary>
/// A high-throughput implementation of <see cref="IFilterPipeline"/> that minimises heap allocations
/// by walking the input as a <see cref="ReadOnlySpan{T}"/> and using stack-allocated buffers for
/// per-word processing.
/// </summary>
public class EfficientFilterPipeline(IEnumerable<IWordFilter> filters, ILogger<EfficientFilterPipeline> logger) : IFilterPipeline
{
    private readonly IWordFilter[] _filters = [.. filters];

    public string Apply(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            logger.LogWarning("Input string is null or whitespace. Returning empty string.");
            return string.Empty;
        }

        var result = new StringBuilder(input.Length);
        var remaining = input.AsSpan();
        bool firstKept = true;

        // Allocate a reusable buffer on the stack for the largest possible word in the input
        int maxWordLength = input.Length;
        Span<char> buffer = stackalloc char[maxWordLength];

        while (!remaining.IsEmpty)
        {
            remaining = remaining.TrimStart(' ');
            if (remaining.IsEmpty) break;

            int wordEnd = remaining.IndexOf(' ');
            ReadOnlySpan<char> token = wordEnd == -1 ? remaining : remaining[..wordEnd];
            remaining = wordEnd == -1 ? ReadOnlySpan<char>.Empty : remaining[(wordEnd + 1)..];

            if (token.IsEmpty) continue;

            int len = 0;
            foreach (char c in token)
            {
                if (char.IsLetterOrDigit(c))
                    buffer[len++] = c;
            }
            ReadOnlySpan<char> stripped = buffer[..len];

            if (PassesAllFilters(stripped))
            {
                if (!firstKept) result.Append(' ');
                result.Append(token);
                firstKept = false;
            }
        }

        return result.ToString();
    }

    private bool PassesAllFilters(ReadOnlySpan<char> word)
    {
        foreach (var filter in _filters)
        {
            if (!filter.ShouldKeep(word))
                return false;
        }
        return true;
    }
}
