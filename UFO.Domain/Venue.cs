namespace UFO.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Venue
    {
        #region Fields

        private int? id;

        #endregion

        public Venue(int id, string shortName, string name, decimal? latitude, decimal? longitude)
        {
            Id = id;
            ShortName = shortName;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        #region Properties

        public int Id
        {
            get
            {
                if (id == null)
                {
                    throw new Exception("This venue doesn't have an Id yet.");
                }

                return id.Value;
            }
            set { id = value; }
        }

        [Range(-90, 90)]
        public decimal? Latitude { get; set; }

        [Range(-180, 180)]
        public decimal? Longitude { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

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
