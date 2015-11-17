namespace UFO.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Performance")]
    public class Performance : DatabaseObject
    {
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
        public DateTime DateTime { get; set; }

        [Column("Venue")]
        [Required]
        public Venue Venue { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            var objCasted = obj as Performance;
            if (objCasted == null)
            {
                return false;
            }
            return objCasted.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            // TODO: DateTime
            return Artist.GetHashCode() ^ Venue.GetHashCode();
        }
    }
}
