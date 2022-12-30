using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Battleships.ViewModel.Command
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<object> _method;

        public CommandBase() { }

        public CommandBase(Action<object> method)
        {
            _method = method;
        }

        public virtual void Execute(object parameter)
        {
            _method?.Invoke(parameter);
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
