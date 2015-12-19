namespace UFO.Commander.Views
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using UFO.Commander.Collections;
    using UFO.Commander.ViewModels;

    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            CreateDayProgramColumns();
        }

        private void CreateDayProgramColumns()
        {
            for (var hour = 13; hour <= 23; ++hour)
            {
                var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                culture.DateTimeFormat.AMDesignator = "AM";
                culture.DateTimeFormat.PMDesignator = "PM";

                var header = DateTime.Today.AddHours(hour).ToString("hh:mm tt", culture) + " - "
                             + DateTime.Today.AddHours(hour + 1).ToString("hh:mm tt", culture);

                var binding = new Binding($"{nameof(VenueProgram.Times)}[{hour}]");

                var column = new DataGridComboBoxColumn
                    {
                        Header = header,
                        SelectedItemBinding = binding,
                        DisplayMemberPath = "Name",
                        ItemsSource = (DataContext as MainViewModel)?.Artists
                    };

                DayProgram.Columns.Add(column);
            }

            DayProgram.GetBindingExpression(ItemsControl.ItemsSourceProperty)?.UpdateTarget();
        }
    }
}
