namespace UFO.Commander.ViewModels
{
    using System;
    using System.Windows.Input;
    using UFO.ViewModels.Interfaces;

    public abstract class ViewModelBase : IRequestCloseViewModel
    {
        #region IRequestCloseViewModel Members

        public event EventHandler RequestClose;

        #endregion

        protected void RaiseRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        #region Nested type: LambdaCommand

        internal class LambdaCommand : ICommand
        {
            #region Fields

            private readonly Action<object> method;

            #endregion

            public LambdaCommand(Action method)
                : this(o => method())
            {
            }

            internal LambdaCommand(Action<object> method)
            {
                this.method = method;
            }

            #region ICommand Members

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter)
            {
                method(parameter);
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            #endregion
        }

        #endregion
    }
}
