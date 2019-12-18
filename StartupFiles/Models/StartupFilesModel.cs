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

        private static readonly RegistryStartupFilesExtractor RegistryStartupFilesExtractor = new RegistryStartupFilesExtractor();
        private static readonly StartMenuStartupFilesExtractor StartMenuStartupFilesExtractor = new StartMenuStartupFilesExtractor();

        public StartupFilesModel()
        {
            StartupFileModels.AddRange(RegistryStartupFilesExtractor.GetStartupFiles());
            StartupFileModels.AddRange(StartMenuStartupFilesExtractor.GetStartupFiles());
        }

        public List<StartupFileModel> StartupFileModels { get; } = new List<StartupFileModel>();

    }
}
