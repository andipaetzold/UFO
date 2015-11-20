namespace UFO.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Performance")]
    public class Performance : DatabaseObject
    {
        #region Fields

        private DateTime dateTime;

        #endregion

        public Performance()
        {
        }

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

        [Column("Artist")]
        [Required]
        public Artist Artist { get; set; }

        [Column("DateTime")]
        [Required]
        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second); }
        }

        [Column("Venue")]
        [Required]
        public Venue Venue { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Performance)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Artist?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ DateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ (Venue?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        protected bool Equals(Performance other)
        {
            return Equals(Artist, other.Artist) && DateTime.Equals(other.DateTime) && Equals(Venue, other.Venue);
        }
    }
}
