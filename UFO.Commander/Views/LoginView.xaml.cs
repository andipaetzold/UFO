namespace UFO.Commander.Views
{
    using System.Windows;
    using UFO.Commander.ViewModels;

    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContextChanged += LoginViewDataContextChanged;
            DataContext = new LoginViewModel();
        }

        private void LoginViewDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = e.NewValue as LoginViewModel;
            if (viewModel != null)
            {
                viewModel.RequestClose += (o, args) => Close();
            }
        }
    }
}
