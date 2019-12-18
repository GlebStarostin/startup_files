using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartupFiles.Models.Interfaces;

namespace StartupFiles.Models
{
    internal class StartupFilesModel
    {

        private static readonly IStartupFilesExtractor[] StartupFilesExtractors =
        {
            new RegistryStartupFilesExtractor(),
            new StartMenuStartupFilesExtractor(),
        };

        public StartupFilesModel()
        {
            StartupFileModels.Clear();
            foreach (var startupFilesExtractor in StartupFilesExtractors)
            {
                StartupFileModels.AddRange(startupFilesExtractor.GetStartupFiles());
            }
        }

        public List<StartupFileModel> StartupFileModels { get; } = new List<StartupFileModel>();

    }
}
