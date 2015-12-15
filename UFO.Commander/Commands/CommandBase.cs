namespace UFO.Commander.Commands
{
    using System;
    using System.Windows.Input;

    public abstract class CommandBase<TViewModel> : ICommand
    {
        #region Fields

        protected readonly TViewModel ViewModel;

        #endregion

        protected CommandBase(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion
    }
}
