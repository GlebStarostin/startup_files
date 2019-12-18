using System.IO;
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
            var fileName = fileNameToParse.Trim();
            var arguments = string.Empty;

            if (fileName.StartsWith(@""""))
            {
                int pos = fileName.IndexOf('"', 1);

                if (pos > 0 && fileName.Length > pos + 1)
                {
                    arguments = fileName.Substring(pos + 1).Trim();
                    fileName = fileName.Substring(0, pos + 1).Trim();
                }
            }
            else
            {
                var pathInvalidChars = Path.GetInvalidPathChars();
                if(fileName.Any(pathInvalidChars.Contains))
                {
                    var pos = fileName.Where(pathInvalidChars.Contains).Select(x => fileName.IndexOf(x)).Min();
                    if (pos > 0 && fileName.Length > pos + 1)
                    {
                        arguments = fileName.Substring(pos + 1).Trim();
                        fileName = fileName.Substring(0, pos + 1).Trim();
                    }
                }
            }

            fileName = fileName.Trim('"');

            return new ParsedResult {FileName = fileName, Arguments = arguments };
        }

    }
}
