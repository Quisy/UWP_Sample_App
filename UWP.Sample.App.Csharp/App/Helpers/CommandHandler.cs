using System;
using System.Windows.Input;

namespace App.Helpers
{
    public class CommandHandler : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _action;

        public CommandHandler(Action action)
        {
            this._action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._action();
        }
    }
}
