namespace UFO.Commander.ViewModels
{
    using System.Windows;
    using System.Windows.Input;
    using UFO.Commander.Views;
    using UFO.Server;

    public class LoginViewModel : ViewModelBase
    {
        #region Fields

        private readonly Server server = new Server();

        #endregion

        public LoginViewModel()
        {
            LoginCommand = new LambdaCommand(Login);
        }

        #region Properties

        public ICommand LoginCommand { get; }
        public string Password { get; set; }
        public string Username { get; set; }

        #endregion

        public void Login()
        {
            if (server.UserServer.CheckLoginData(Username, Password))
            {
                new MainView { DataContext = new MainViewModel() }.Show();
                RaiseRequestClose();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}
