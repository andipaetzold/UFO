namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Category : DatabaseObject
    {
        public Category(int id, string name)
            : this(name)
        {
            InsertedInDatabase(id);
        }

        public Category(string name)
        {
            Name = name;
        }

        #region Properties

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
