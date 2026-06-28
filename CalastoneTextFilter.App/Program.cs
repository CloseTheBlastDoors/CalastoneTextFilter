using CalastoneTextFilter.App.FilterPipeline;
using CalastoneTextFilter.App.FilterPipeline.Filters;

if (args.Length == 0)
{
    Console.Error.WriteLine("Usage: TextFilter.App <path-to-file.txt>");
    return 1;
}

string path = args[0];

if (!File.Exists(path))
{
    Console.Error.WriteLine($"File not found: {path}");
    return 1;
}

string text = File.ReadAllText(path);

const int MIN_LENGTH = 3;
const char CHARACTER_TO_FILTER = 't';

var filterPipeline = new FilterPipeline(
[
    new MiddleVowelFilter(),
    new MinLengthFilter(MIN_LENGTH),
    new ContainsCharacterFilter(CHARACTER_TO_FILTER)
]);

string result = filterPipeline.Apply(text);

Console.WriteLine(result);
return 0;
