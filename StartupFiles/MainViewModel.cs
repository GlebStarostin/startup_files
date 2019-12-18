using System;
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
                execute: param =>
                {
                    IsStartupFilesLoading = true;

                    StartupFilesCount = 0;
                    var progressReporter = new Progress<int>(
                        startupFilesAdded => StartupFilesCount += startupFilesAdded);
                    StartupFiles = new StartupFilesModel(progressReporter);

                    IsStartupFilesLoading = false;
                });
        }

        private bool _isStartupFilesLoading;
        public bool IsStartupFilesLoading
        {
            get { return _isStartupFilesLoading; }
            set
            {
                _isStartupFilesLoading = value;
                OnPropertyChanged(nameof(MainViewModel.IsStartupFilesLoading));
            }
        }

        private volatile int _startupFilesCount;
        public int StartupFilesCount
        {
            get { return _startupFilesCount; }
            set
            {
                _startupFilesCount = value;
                OnPropertyChanged(nameof(MainViewModel.StartupFilesCount));
            }
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
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
