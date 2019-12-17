using System.Drawing;

namespace StartupFiles.Models
{
    internal class StartupFileModel
    {

        public Icon Icon { get; set; }

        public string FileName { get; set; }

        public string Arguments { get; set; }

        public string FileDirectory { get; set; }

        public StartupType StartupType { get; set; }

    }
}
