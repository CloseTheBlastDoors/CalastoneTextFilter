namespace CalastoneTextFilter.Core.FilterPipeline;

/// <summary>
/// Defines a method which applies a series of word filters to an input string, returning a new string containing only the words that pass all filters.
/// </summary>
public interface IFilterPipeline
{
    /// <summary>
    /// Call this function with the full input text to process
    /// </summary>
    /// <param name="input">The full set of text to process</param>
    /// <returns>Where every word that didn't pass filtering has been removed</returns>
    string Apply(string input);
}
