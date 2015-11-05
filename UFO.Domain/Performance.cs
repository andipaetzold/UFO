namespace UFO.Domain
{
    using System;

    public class Performance
    {
        #region Fields

        private int? id;

        #endregion

        public Performance(int id, DateTime dateTime, Artist artist, Venue venue)
            : this(dateTime, artist, venue)
        {
            Id = id;
        }

        public Performance(DateTime dateTime, Artist artist, Venue venue)
        {
            DateTime = dateTime;
            Artist = artist;
            Venue = venue;
        }

        #region Properties

        public Artist Artist { get; set; }
        public DateTime DateTime { get; set; }

        public int Id
        {
            get
            {
                if (id == null)
                {
                    throw new Exception("This performance doesn't have an Id yet.");
                }

                return id.Value;
            }
            set { id = value; }
        }

        public Venue Venue { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Artist.GetHashCode() ^ DateTime.GetHashCode() ^ Venue.GetHashCode();
        }
    }
}
