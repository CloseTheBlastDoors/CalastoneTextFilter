using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using CalastoneTextFilter.Core.FilterPipeline;

var serviceCollection = new ServiceCollection()
    .AddLogging(builder => builder.AddConsole());

serviceCollection.AddFilterPipeline(pipeline => pipeline
    .AddMiddleVowelFilter()
    .AddMinLengthFilter(3)
    .AddCharacterFilter('t'));

var serviceProvider = serviceCollection.BuildServiceProvider();
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

string? path = GetFilePathFromArgs(args);
if (path is null) return 1;

string text = File.ReadAllText(path);

var pipeline = serviceProvider.GetRequiredService<IFilterPipeline>();
string result = pipeline.Apply(text);

Console.WriteLine(result);
return 0;

string? GetFilePathFromArgs(string[] args)
{
    if (args.Length == 0)
    {
        logger.LogError("Usage: TextFilter.App <path-to-file.txt>");
        return null;
    }

    string path = args[0];

    if (!File.Exists(path))
    {
        logger.LogError("File not found: {Path}", path);
        return null;
    }

    return path;
}
