namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Artist")]
    public class Artist : DatabaseObject
    {
        public Artist(
            int id,
            string name,
            Category category,
            Country country,
            string image,
            string email,
            string videoUrl)
            : this(name, category, country, image, email, videoUrl)
        {
            Id = id;
        }

        public Artist()
        {
        }

        public Artist(string name, Category category, Country country, string image, string email, string videoUrl)
        {
            Name = name;
            Category = category;
            Country = country;
            Image = image;
            Email = email;
            VideoUrl = videoUrl;
        }

        #region Properties

        [Column("Category")]
        [Required]
        public Category Category { get; set; }

        [Column("Country")]
        [Required]
        public Country Country { get; set; }

        [Column("Email")]
        [StringLength(128)]
        [EmailAddress]
        public string Email { get; set; }

        [Column("Image")]
        [StringLength(128)]
        public string Image { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Column("VideoUrl")]
        [StringLength(128)]
        public string VideoUrl { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            var objCasted = obj as Artist;
            if (objCasted == null)
            {
                return false;
            }
            return objCasted.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return (Name?.GetHashCode() ?? 0) ^ (Category?.GetHashCode() ?? 0) ^ (Country?.GetHashCode() ?? 0)
                   ^ (Image?.GetHashCode() ?? 0) ^ (Email?.GetHashCode() ?? 0) ^ (VideoUrl?.GetHashCode() ?? 0);
        }
    }
}
