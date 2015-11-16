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
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
