using System;
using System.Windows.Input;

namespace StartupFiles.Models
{
    internal class Command : ICommand
    {

        public event EventHandler CanExecuteChanged;

        private readonly Action<object> _execute;

        public Command(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }

    }
}
