namespace UFO.Commander.Collections
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using UFO.Domain;
    using UFO.Server.Implementation;

    public class VenueProgram
    {
        public VenueProgram()
        {
            Times = new Dictionary<int, Performance>();
        }

        #region Properties

        public static Artist NullArtist { get; } = new Artist("", null, null, null, null, null, false);
        public Dictionary<int, Performance> Times { get; }
        public Venue Venue { get; set; }

        #endregion

        public static List<VenueProgram> Create(DateTime date, IList<Performance> performances, IList<Venue> venues)
        {
            date = date.Date;

            var list = new List<VenueProgram>();

            foreach (var venue in venues)
            {
                var venueProgram = new VenueProgram { Venue = venue };

                for (var hour = 0; hour <= 23; ++hour)
                {
                    var performance =
                        performances.FirstOrDefault(p => (p.DateTime.Hour == hour) && (p.Venue.Equals(venue)));

                    if (performance == null)
                    {
                        performance = new Performance
                            {
                                Artist = NullArtist,
                                DateTime = date.AddHours(hour),
                                Venue = venue
                            };
                    }

                    performance.PropertyChanged += PerformancePropertyChanged;

                    venueProgram.Times[hour] = performance;
                }

                list.Add(venueProgram);
            }

            return list;
        }

        private static void PerformancePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var performance = (Performance)sender;

            // only artist
            if (e.PropertyName != nameof(performance.Artist))
            {
                return;
            }

            // push to server
            if (performance.HasId && !performance.Artist.Equals(NullArtist))
            {
                if (Server.PerformanceServer.Update(performance))
                {
                    performance.PropertyChanged -= PerformancePropertyChanged;
                    performance.Artist = Server.PerformanceServer.GetById(performance.Id).Artist;
                    performance.PropertyChanged += PerformancePropertyChanged;

                    MessageBox.Show(
                        "Invalid data. The item will be reset",
                        "Invalid data.",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else if (performance.HasId && performance.Artist.Equals(NullArtist))
            {
                Server.PerformanceServer.Remove(performance);
            }
            else if (!performance.Artist.Equals(NullArtist))
            {
                if (!Server.PerformanceServer.Add(performance))
                {
                    performance.PropertyChanged -= PerformancePropertyChanged;
                    performance.Artist = NullArtist;
                    performance.PropertyChanged += PerformancePropertyChanged;

                    MessageBox.Show(
                        "Invalid data. The item will be reset",
                        "Invalid data.",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }
    }
}
