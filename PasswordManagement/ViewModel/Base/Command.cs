using System;
using System.Windows.Input;

namespace PasswordManagement.ViewModel.Base
{
    /// <summary>
    /// Delivers a <see cref="ICommand"/> implementation for Binding Commands to functions
    /// </summary>
    public class Command : ICommand
    {
        private readonly Func<object, bool> canExecute;
        private readonly Action<object> target;

        public Command(Action<object> target)
        {
            this.target = target;
        }

        public Command(Action<object> target, Func<object, bool> canExecute) : this(target)
        {
            this.canExecute = canExecute;
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Validation, if the Command can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        /// <summary>
        /// Execute the command, if <see cref="CanExecute"/> is True
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter)) target(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}