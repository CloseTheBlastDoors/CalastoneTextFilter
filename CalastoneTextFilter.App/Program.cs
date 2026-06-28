using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using CalastoneTextFilter.App.FilterPipeline;
using CalastoneTextFilter.App.FilterPipeline.Filters;

var serviceCollection = new ServiceCollection()
    .AddLogging(builder =>
    {
        builder.AddConsole();
    });

var serviceProvider = serviceCollection.BuildServiceProvider();
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

if (args.Length == 0)
{
    logger.LogError("Usage: TextFilter.App {Path}", "<path-to-file.txt>");
    return 1;
}

string path = args[0];

if (!File.Exists(path))
{
    logger.LogError("File not found: {Path}", path);
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

logger.LogInformation("{Result}", result);
return 0;
