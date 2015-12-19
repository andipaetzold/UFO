namespace UFO.Commander.Views
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;
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
                        ItemsSource = ((MainViewModel)DataContext).ArtistsWithNull,
                        CellStyle =
                            new Style
                                {
                                    Setters =
                                        {
                                            new Setter
                                                {
                                                    Property = BackgroundProperty,
                                                    Value =
                                                        new Binding(
                                                            $"{nameof(VenueProgram.Times)}[{hour}].{nameof(Artist)}.{nameof(Category)}")
                                                            {
                                                                Converter = new CategoryToBrushConverter()
                                                            }
                                                }
                                        }
                                }
                    };

                DayProgram.Columns.Add(column);
            }
        }

        #region Nested type: CategoryToBrushConverter

        private class CategoryToBrushConverter : IValueConverter
        {
            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value == null)
                {
                    return new SolidColorBrush(Color.FromRgb(255, 255, 255));
                }
                var bytes = BitConverter.GetBytes(value.GetHashCode());
                return new SolidColorBrush(Color.FromRgb(bytes[0], bytes[1], bytes[2]));
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        #endregion
    }
}
