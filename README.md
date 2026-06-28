# CalastoneTextFilter

I have completed my submission.

This is a console app - the first command line argument is the path to the text file to process.
As instructed the output is written to the console.

The text input from the assignment has been included in the project as 'Input.txt' and the debug options on the project are configured to automatically process it.

Here is further work I would do with more time and with further feedback from the spec owners:

* I have made sure punctuation doesn't interfere with the processing of words - i.e. I strip out any punctuation before passing into the word filter
- However, the punctuation is kept in the output. Would seek clarification on whether this is right (the spec doesn't mention punctuation at all...)

* I would extend the tests to cover other settings on the 'ContainsCharacter' and 'MinLength' filters. In the interests of time, I stuck to the values in the spec: (3 Min Length, character 't')

* Currently the 'FilterPipeline' implementation is inside the console app project. Ideally, the implemenation and the tests should be split out into a class library and then referenced by the app

* I would add \\\summary sections to the most important public methods (at least the ServiceCollectionExtensions methods and IFilterPipeline:Apply()

* The current implementation is appropriate for the stated use case. If this were deployed as a high-throughput service (e.g. processing above ~10k req/s), it might be worth revisiting Apply to walk the input as a ReadOnlySpan<char> and eliminate the per-word heap allocations. IWordFilter is already set up to accept ReadOnlySpan<char> rather than string, so hopefully the individual filter interface and implementations shouldn't need to be touched as part of this refactoring.
The 'efficient' version of FilterPipeline could be implementated as a different class (e.g. EfficientFilterPipeline) and then all that would be needed for the console app to switch to efficient processing would be to change the DI line for the FilterPipeline (i.e serviceCollection.AddEfficientFilterPipeline())