namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Venue : DatabaseObject
    {
        public Venue(int id, string shortName, string name, decimal? latitude, decimal? longitude)
            : this(shortName, name, latitude, longitude)
        {
            InsertedInDatabase(id);
        }

        public Venue(string shortName, string name, decimal? latitude, decimal? longitude)
        {
            ShortName = shortName;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        #region Properties

        [Range(-90, 90)]
        public decimal? Latitude { get; set; }

        [Range(-180, 180)]
        public decimal? Longitude { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

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
