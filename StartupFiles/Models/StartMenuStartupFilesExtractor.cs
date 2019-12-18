using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using StartupFiles.Models.Interfaces;
using StartupFiles.Models.Utils;

namespace StartupFiles.Models
{
    internal class StartMenuStartupFilesExtractor : IStartupFilesExtractor
    {

        private static readonly string[] StartMenuStartupFolders = {
            Environment.GetFolderPath(Environment.SpecialFolder.Startup),
            Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup),
        };

        public IEnumerable<StartupFileModel> GetStartupFiles(IProgress<int> progressReporter = null)
        {
            var result = new List<StartupFileModel>();
            foreach (var startMenuStartupFolder in StartMenuStartupFolders)
            {
                var files = Directory.GetFiles(startMenuStartupFolder);
                foreach (var fileName in files)
                {
                    var fileInfo = new FileInfo(fileName);
                    switch (fileInfo.Extension)
                    {
                        case ".exe":
                            result.Add(GetModelFromExecutable(fileName));
                            progressReporter?.Report(1);
                            break;
                        case ".lnk":
                            result.Add(GetModelFromShortcut(fileName));
                            progressReporter?.Report(1);
                            break;
                        case ".ini":
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
            return result;
        }

        private StartupFileModel GetModelFromShortcut(string shortcutFileName)
        {
            var shortcutInfo = ShortcutUtils.ResolveShortcut(shortcutFileName);
            return new StartupFileModel
            {
                FileDirectory = Path.GetDirectoryName(shortcutInfo.Path),
                FileName = Path.GetFileName(shortcutInfo.Path),
                Arguments = shortcutInfo.Arguments,
                Icon = Icon.ExtractAssociatedIcon(shortcutFileName),
                StartupType = StartupType.StartMenu,
            };
        }

        private StartupFileModel GetModelFromExecutable(string executableFileName)
        {
            return new StartupFileModel
            {
                FileDirectory = Path.GetDirectoryName(executableFileName),
                FileName = Path.GetFileName(executableFileName),
                Arguments = String.Empty,
                Icon = Icon.ExtractAssociatedIcon(executableFileName),
                StartupType = StartupType.StartMenu,
            };
        }

    }
}
