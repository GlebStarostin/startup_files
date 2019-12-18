using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using StartupFiles.Models.Interfaces;
using StartupFiles.Models.Utils;

namespace StartupFiles.Models
{
    internal class RegistryStartupFilesExtractor : IStartupFilesExtractor
    {

        private static readonly string[] RegistryStartupFilesSubKeysNames = {
            @"Software\Microsoft\Windows\CurrentVersion\Run",
            @"Software\Microsoft\Windows\CurrentVersion\RunOnce",
            @"Software\Microsoft\Windows\CurrentVersion\RunServices",
            @"Software\Microsoft\Windows\CurrentVersion\RunServicesOnce",
            @"Software\Microsoft\Windows NT\CurrentVersion\Winlogon\Userinit",
            @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Run",
            @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\RunOnce",
            @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\RunServices",
            @"Software\Wow6432Node\Microsoft\Windows\CurrentVersion\RunServicesOnce",
            @"Software\Wow6432Node\Microsoft\Windows NT\CurrentVersion\Winlogon\Userinit",
        };

        public IEnumerable<StartupFileModel> GetStartupFiles()
        {
            var result = GetValuesFromBaseLevel(Registry.LocalMachine);
            result.AddRange(GetValuesFromBaseLevel(Registry.CurrentUser));
            return result;
        }

        private List<StartupFileModel> GetValuesFromBaseLevel(RegistryKey baseLevelKey)
        {
            var result = new List<StartupFileModel>();
            foreach (var registryStartupFilesSubKeyName in RegistryStartupFilesSubKeysNames)
            {
                using (var registryStartupFilesSubKey = baseLevelKey.OpenSubKey(registryStartupFilesSubKeyName, writable: false))
                {
                    if (registryStartupFilesSubKey == null)
                        continue;

                    var valueNames = registryStartupFilesSubKey.GetValueNames();
                    foreach (var valueName in valueNames)
                    {
                        var value = registryStartupFilesSubKey.GetValue(valueName, null);
                        if (value == null)
                            continue;

                        var fileInfo = ArgumentsParser.SplitArgumentsAndFileName(value.ToString());
                        if (string.IsNullOrWhiteSpace(fileInfo?.FileName))
                            continue;

                        result.Add(new StartupFileModel
                        {
                            FileDirectory = Path.GetDirectoryName(fileInfo.FileName),
                            FileName = Path.GetFileName(fileInfo.FileName),
                            Arguments = fileInfo.Arguments,
                            Icon = Icon.ExtractAssociatedIcon(fileInfo.FileName),
                            StartupType = StartupType.Registry,
                        });
                    }
                }
            }
            return result;
        }

    }
}
