namespace UFO.Commander.Views
{
    using System.Windows;
    using UFO.Commander.ViewModels;

    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
