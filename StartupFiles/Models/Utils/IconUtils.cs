using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace StartupFiles.Models.Utils
{
    internal static class IconUtils
    {

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int ExtractIconEx(
            string lpszFile,
            int nIconIndex,
            IntPtr[] phIconLarge,
            IntPtr[] phIconSmall,
            int nIcons);

        [DllImport("user32.dll", EntryPoint = "DestroyIcon", SetLastError = true)]
        private static extern int DestroyIcon(IntPtr hIcon);

        public static Icon DefaultExeIcon { get; } = null;

        public static Icon ExtractIconFromExe(string file, bool large = true)
        {
            IntPtr[] unused = { IntPtr.Zero };
            IntPtr[] iconHandles = { IntPtr.Zero };

            try
            {
                int extractedIconsCount;
                if (large)
                    extractedIconsCount = ExtractIconEx(file, 0, iconHandles, unused, 1);
                else
                    extractedIconsCount = ExtractIconEx(file, 0, unused, iconHandles, 1);

                var firstIconHandle = iconHandles[0];
                if (extractedIconsCount > 0 && firstIconHandle != IntPtr.Zero)
                {
                    var extractedIcon = Icon.FromHandle(firstIconHandle).Clone() as Icon;
                    return extractedIcon;
                }
                else
                    return DefaultExeIcon;
            }
            catch (Exception)
            {
                return DefaultExeIcon;
            }
            finally
            {
                foreach (var ptr in iconHandles)
                    if (ptr != IntPtr.Zero)
                        DestroyIcon(ptr);

                foreach (var ptr in unused)
                    if (ptr != IntPtr.Zero)
                        DestroyIcon(ptr);
            }
        }

    }
}
