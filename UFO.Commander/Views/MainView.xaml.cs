namespace UFO.Commander.Views
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using UFO.Commander.Util;
    using UFO.Commander.ViewModels;
    using UFO.Domain;

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

                var binding = new Binding($"{nameof(VenueProgram.Times)}[{hour}].{nameof(Performance.Artist)}")
                    {
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    };

                var column = new DataGridComboBoxColumn
                    {
                        Header = header,
                        SelectedItemBinding = binding,
                        DisplayMemberPath = nameof(Artist.Name),
                        ItemsSource = ((MainViewModel)DataContext).ArtistsWithNull
                    };

                DayProgram.Columns.Add(column);
            }
        }
    }
}
