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
            string videoUrl,
            bool isDeleted)
            : this(name, category, country, image, email, videoUrl, isDeleted)
        {
            Id = id;
        }

        public Artist()
        {
        }

        public Artist(
            string name,
            Category category,
            Country country,
            string image,
            string email,
            string videoUrl,
            bool isDeleted)
        {
            Name = name;
            Category = category;
            Country = country;
            Image = image;
            Email = email;
            VideoUrl = videoUrl;
            IsDeleted = isDeleted;
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

        [Column("IsDeleted")]
        [Required]
        public bool IsDeleted { get; set; }

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
            return Equals((Artist)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Category?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Country?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Email?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Image?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ IsDeleted.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (VideoUrl?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        protected bool Equals(Artist other)
        {
            return Equals(Category, other.Category) && Equals(Country, other.Country)
                   && string.Equals(Email, other.Email) && string.Equals(Image, other.Image)
                   && IsDeleted == other.IsDeleted && string.Equals(Name, other.Name)
                   && string.Equals(VideoUrl, other.VideoUrl);
        }
    }
}
