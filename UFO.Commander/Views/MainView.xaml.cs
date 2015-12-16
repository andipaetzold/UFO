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
            return;
            for (var hour = 0; hour <= 23; ++hour)
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
                        ElementStyle =
                            new Style(typeof(ComboBox))
                                {
                                    Setters =
                                        {
                                            new Setter
                                                {
                                                    Property = ItemsControl.ItemsSourceProperty,
                                                    Value =
                                                        new Binding
                                                            {
                                                                Path = new PropertyPath("DataContext.Artists"),
                                                                RelativeSource = new RelativeSource { AncestorType = typeof(MainViewModel) },
                                                                ValidatesOnDataErrors = true
                                                            }
                                                }
                                        }
                                },
                        EditingElementStyle =
                            new Style(typeof(ComboBox))
                                {
                                    Setters =
                                        {
                                            new Setter
                                                {
                                                    Property = ItemsControl.ItemsSourceProperty,
                                                    Value =
                                                        new Binding
                                                            {
                                                                Path = new PropertyPath("DataContext.Artists"),
                                                                RelativeSource = new RelativeSource { AncestorType = typeof(MainViewModel) },
                                                                ValidatesOnDataErrors = true
                                                            }
                                                }
                                        }
                                }
                    };

                DayProgram.Columns.Add(column);
            }
        }
    }
}
