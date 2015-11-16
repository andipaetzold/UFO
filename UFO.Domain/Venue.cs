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
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return ShortName.GetHashCode() ^ Name.GetHashCode() ^ Latitude.GetHashCode() ^ Longitude.GetHashCode();
        }
    }
}
