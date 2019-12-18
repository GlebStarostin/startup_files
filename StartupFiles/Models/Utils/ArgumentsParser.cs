using System;
using System.Linq;

namespace StartupFiles.Models.Utils
{
    internal static class ArgumentsParser
    {

        public class ParsedResult
        {
            public string FileName { get; set; }
            public string Arguments { get; set; } = string.Empty;
        }

        public static ParsedResult SplitArgumentsAndFileName(string fileNameToParse)
        {
            var fileName = fileNameToParse;
            var arguments = string.Empty;

            var foldersAndFileName = fileNameToParse.Split(new[] {@"\"}, StringSplitOptions.RemoveEmptyEntries);
            var localFileNameAndArguments = foldersAndFileName.Last();
            var possibleArguments = localFileNameAndArguments.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (possibleArguments.Length > 1)
            {
                var argumentsArray = possibleArguments.Skip(1).ToArray();
                arguments = argumentsArray.Skip(1)
                    .Aggregate(seed: argumentsArray.First(), func: (seed, argument) => $"{seed} {argument}");
                fileName = foldersAndFileName.Skip(1).Take(foldersAndFileName.Length - 2)
                    .Aggregate(seed: foldersAndFileName.First(), func: (seed, argument) => $@"{seed}\{argument}",
                    resultSelector: aggregatedArguments => $@"{aggregatedArguments}\{possibleArguments.First()}")
                    .Trim('"');
            }

            return new ParsedResult {FileName = fileName, Arguments = arguments };
        }

    }
}
