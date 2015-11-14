namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Artist : DatabaseObject
    {
        public Artist(int id, string name, Category category, string image, string email, string videoUrl)
            : this(name, category, image, email, videoUrl)
        {
            InsertedInDatabase(id);
        }

        public Artist()
        {
        }

        public Artist(string name, Category category, string image, string email, string videoUrl)
        {
            Name = name;
            Category = category;
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

        [StringLength(128)]
        public string Image { get; set; }

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
