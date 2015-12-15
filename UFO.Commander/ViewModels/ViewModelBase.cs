namespace UFO.Commander.ViewModels
{
    using System;
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
    }
}
