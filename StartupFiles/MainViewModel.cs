using System.ComponentModel;
using System.Windows.Input;
using StartupFiles.Models;

namespace StartupFiles
{
    internal class MainViewModel : INotifyPropertyChanged
    {

        public MainViewModel()
        {
            FillStartupFilesCommand = new TaskRunCommand(
                execute: param => StartupFiles = new StartupFilesModel());
        }

        public ICommand FillStartupFilesCommand { get; }

        private StartupFilesModel _startupFiles;
        public StartupFilesModel StartupFiles
        {
            get { return _startupFiles; }
            set
            {
                _startupFiles = value;
                OnPropertyChanged(nameof(MainViewModel.StartupFiles));
                OnPropertyChanged(nameof(MainViewModel.StartupFilesCount));
            }
        }

        public int StartupFilesCount => StartupFiles?.StartupFileModels?.Count ?? 0;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
