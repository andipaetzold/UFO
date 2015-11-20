namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Venue")]
    public class Venue : DatabaseObject
    {
        public Venue()
        {
        }

        public Venue(int id, string shortName, string name, decimal? latitude, decimal? longitude)
            : this(shortName, name, latitude, longitude)
        {
            Id = id;
        }

        public Venue(string shortName, string name, decimal? latitude, decimal? longitude)
        {
            ShortName = shortName;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        #region Properties

        [Column("Latitude")]
        [Range(-90, 90)]
        public decimal? Latitude { get; set; }

        [Column("Longitude")]
        [Range(-180, 180)]
        public decimal? Longitude { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column("ShortName")]
        [Required]
        [StringLength(8)]
        public string ShortName { get; set; }

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
            return Equals((Venue)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ShortName?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        protected bool Equals(Venue other)
        {
            return Latitude == other.Latitude && Longitude == other.Longitude && string.Equals(Name, other.Name)
                   && string.Equals(ShortName, other.ShortName);
        }
    }
}
