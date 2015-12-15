namespace UFO.Commander
{
    using System.Windows;
    using UFO.Commander.ViewModels;
    using UFO.Commander.Views;

    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            var v = new LoginView { DataContext = new LoginViewModel() };
            v.Show();
        }
    }
}
