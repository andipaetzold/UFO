namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Category")]
    public class Category : DatabaseObject
    {
        public Category(int id, string name)
            : this(name)
        {
            Id = id;
        }

        public Category()
        {
        }

        public Category(string name)
        {
            Name = name;
        }

        #region Properties

        [Column("Name")]
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

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
            return Equals((Category)obj);
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }

        protected bool Equals(Category other)
        {
            return string.Equals(Name, other.Name);
        }
    }
}
