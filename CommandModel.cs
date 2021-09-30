using System;
using System.Windows.Input;

namespace Resolver
{
    class CommandModel : ICommand
    {
        private readonly Func<object, bool> _can_execute;
        private readonly Action<object> _execute;

        internal CommandModel(Action<object> execute, Func<object, bool> can_exucute = null) =>
            (_can_execute, _execute) = (can_exucute, execute);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) =>
            _can_execute == null || _can_execute(parameter);

        public void Execute(object parameter) => _execute(parameter);
    }
}
