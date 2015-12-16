namespace UFO.Commander.Collections
{
    using System.Collections.Generic;
    using System.Linq;
    using UFO.Domain;

    public class VenueProgram
    {
        public VenueProgram()
        {
            Times = new Dictionary<int, Artist>();
        }

        #region Properties

        public Dictionary<int, Artist> Times { get; }
        public Venue Venue { get; set; }

        #endregion

        public static List<VenueProgram> Create(IList<Performance> performances, IList<Venue> venues)
        {
            var list = new List<VenueProgram>();

            foreach (var venue in venues)
            {
                var venueProgram = new VenueProgram();
                venueProgram.Venue = venue;

                for (var hour = 0; hour <= 23; ++hour)
                {
                    venueProgram.Times[hour] =
                        performances.FirstOrDefault(p => (p.DateTime.Hour == hour) && (p.Venue.Equals(venue)))?.Artist;
                }

                list.Add(venueProgram);
            }

            return list;
        }
    }
}
