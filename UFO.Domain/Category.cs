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
            var objCasted = obj as Category;
            if (objCasted == null)
            {
                return false;
            }
            return objCasted.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
