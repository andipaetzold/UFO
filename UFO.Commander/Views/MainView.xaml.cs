namespace UFO.Commander.Views
{
    using System.Windows;
    using UFO.Commander.ViewModels;

    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            DataContextChanged += MainViewDataContextChanged;
            DataContext = new MainViewModel();
        }

        private void MainViewDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            if (viewModel == null)
            {
            }
        }
    }
}
