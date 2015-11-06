namespace UFO.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Artist
    {
        #region Fields

        private int? id;

        #endregion

        public Artist(int id, string name, string imageFileName, string email, string videoUrl)
            : this(name, imageFileName, email, videoUrl)
        {
            Id = id;
        }

        public Artist(string name, string imageFileName, string email, string videoUrl)
        {
            Name = name;
            ImageFileName = imageFileName;
            Email = email;
            VideoUrl = videoUrl;
        }

        #region Properties

        [StringLength(128)]
        public string Email { get; set; }

        public int Id
        {
            get
            {
                if (id == null)
                {
                    throw new Exception("This artist doesn't have an Id yet.");
                }

                return id.Value;
            }
            set { id = value; }
        }

        [StringLength(128)]
        public string ImageFileName { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)]
        public string VideoUrl { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ ImageFileName.GetHashCode() ^ Email.GetHashCode() ^ VideoUrl.GetHashCode();
        }
    }
}
