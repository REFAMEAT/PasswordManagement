using System;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using REFame.PasswordManagement.Logging;

namespace REFame.PasswordManagement.WpfBase
{
    public class AsyncCommand : ICommand
    {
        private readonly Func<object, Task> execute;

        [CanBeNull]
        private readonly Func<bool> canExecute;
        private bool isExecuting;

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null) : 
            this(x => execute(), canExecute)
        {
            
        }

        public AsyncCommand(Func<object, Task> executeFunc, Func<bool> canExecuteFunc = null)
        {
            execute = executeFunc;
            canExecute = canExecuteFunc;
        }

        public bool CanExecute(object parameter)
        {
            if (isExecuting)
            {
                return false;
            }

            return canExecute == null || canExecute();
        }

        public async void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    isExecuting = true;
                    await execute(parameter);
                }
                catch (Exception ex)
                {
                    Logger.Current.Get().Error(ex);
                }
                finally
                {
                    isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}