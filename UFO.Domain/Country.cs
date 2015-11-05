namespace UFO.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        #region Fields

        private int? id;

        #endregion

        public Country(int id, string name)
            : this(name)
        {
            Id = id;
        }

        public Country(string name)
        {
            Name = name;
        }

        #region Properties

        public int Id
        {
            get
            {
                if (id == null)
                {
                    throw new Exception("This country doesn't have an Id yet.");
                }

                return id.Value;
            }
            set { id = value; }
        }

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
