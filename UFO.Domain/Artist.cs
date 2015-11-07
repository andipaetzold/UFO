namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Artist : DatabaseObject
    {
        public Artist(int id, string name, string imageFileName, string email, string videoUrl)
            : this(name, imageFileName, email, videoUrl)
        {
            InsertedInDatabase(id);
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
            return (Name?.GetHashCode() ?? 0) ^ (ImageFileName?.GetHashCode() ?? 0) ^ (Email?.GetHashCode() ?? 0)
                   ^ (VideoUrl?.GetHashCode() ?? 0);
        }
    }
}
