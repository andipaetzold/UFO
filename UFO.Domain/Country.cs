namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Country")]
    public class Country : DatabaseObject
    {
        public Country(int id, string name)
            : this(name)
        {
            Id = id;
        }

        public Country(string name)
        {
            Name = name;
        }

        public Country()
        {
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
            return Equals((Country)obj);
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }

        protected bool Equals(Country other)
        {
            return string.Equals(Name, other.Name);
        }
    }
}
