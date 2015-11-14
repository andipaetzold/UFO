namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Artist : DatabaseObject
    {
        public Artist(int id, string name, Category category, byte[] image, string email, string videoUrl)
            : this(name, category, image, email, videoUrl)
        {
            InsertedInDatabase(id);
        }

        public Artist(string name, Category category, byte[] image, string email, string videoUrl)
        {
            Name = name;
            Image = image;
            Email = email;
            VideoUrl = videoUrl;
        }

        #region Properties

        [Required]
        public Category Category { get; set; }

        [StringLength(128)]
        [EmailAddress]
        public string Email { get; set; }

        public byte[] Image { get; set; }

        [Required]
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
            return (Name?.GetHashCode() ?? 0) ^ (Category?.GetHashCode() ?? 0) ^ (Image?.GetHashCode() ?? 0)
                   ^ (Email?.GetHashCode() ?? 0) ^ (VideoUrl?.GetHashCode() ?? 0);
        }
    }
}
