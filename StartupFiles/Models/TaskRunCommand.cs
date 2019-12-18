using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace StartupFiles.Models
{
    internal class TaskRunCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private readonly Action<object> _execute;

        private volatile bool _isExecuting;
        public bool IsExecuting
        {
            get { return _isExecuting; }
            set
            {
                _isExecuting = value;
                RaiseCanExecuteChanged();
            }
        }

        public TaskRunCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return !IsExecuting;
        }

        private readonly object _executeCheckLock = new object();
        public void Execute(object parameter)
        {
            lock (_executeCheckLock)
            {
                if (IsExecuting)
                    return;
                IsExecuting = true;
            }
            Task.Run(() =>
            {
                _execute?.Invoke(parameter);
            })
            .ContinueWith(task =>
            {
                lock (_executeCheckLock)
                {
                    IsExecuting = false;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
