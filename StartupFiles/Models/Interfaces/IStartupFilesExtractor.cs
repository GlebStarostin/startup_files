using System.Collections.Generic;

namespace StartupFiles.Models.Interfaces
{
    internal interface IStartupFilesExtractor
    {
        IEnumerable<string> GetStartupFileNames();
    }
}
