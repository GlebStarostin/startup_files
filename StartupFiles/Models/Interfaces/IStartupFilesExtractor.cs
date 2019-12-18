using System;
using System.Collections.Generic;

namespace StartupFiles.Models.Interfaces
{
    internal interface IStartupFilesExtractor
    {
        IEnumerable<StartupFileModel> GetStartupFiles(IProgress<int> progressReporter = null);
    }
}
