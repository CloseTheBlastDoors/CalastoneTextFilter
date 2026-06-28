using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using CalastoneTextFilter.App.FilterPipeline;
using CalastoneTextFilter.App.FilterPipeline.Filters;       

var serviceCollection = new ServiceCollection()
    .AddLogging(builder =>
    {
        builder.AddConsole();
    });

const int MIN_LENGTH = 3;
const char CHARACTER_TO_FILTER = 't';

serviceCollection.AddFilterPipeline();
serviceCollection.AddMinLengthFilter(MIN_LENGTH);
serviceCollection.AddCharacterFilter(CHARACTER_TO_FILTER);
serviceCollection.AddMiddleVowelFilter();

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

var processor = serviceProvider.GetRequiredService<IFilterPipeline>();

string result = processor.Apply(text);
logger.LogTrace("{Result}", result);
return 0;
