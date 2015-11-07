namespace UFO.Domain
{
    using System;

    public class Performance : DatabaseObject
    {
        public Performance(int id, DateTime dateTime, Artist artist, Venue venue)
            : this(dateTime, artist, venue)
        {
            InsertedInDatabase(id);
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
        public Venue Venue { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            // TODO: DateTime
            return Artist.GetHashCode() ^ Venue.GetHashCode();
        }
    }
}
