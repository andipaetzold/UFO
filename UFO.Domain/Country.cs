namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class Country : DatabaseObject
    {
        public Country(int id, string name)
            : this(name)
        {
            InsertedInDatabase(id);
        }

        public Country(string name)
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
