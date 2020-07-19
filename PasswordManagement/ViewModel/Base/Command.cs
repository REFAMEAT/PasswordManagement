using System;
using System.Windows.Input;

namespace PasswordManagement.ViewModel.Base
{
    public class Command : ICommand
    {
        private readonly Action<object> target;
        private readonly Func<object,bool> canExecute;

        public Command(Action<object> target)
        {
            this.target = target;
        }
        public Command(Action<object> target, Func<object, bool> canExecute) : this(target)
        {
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                target(parameter); 
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}