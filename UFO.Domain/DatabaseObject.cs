namespace UFO.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics;

    public abstract class DatabaseObject
    {
        #region Fields

        private int? id;

        #endregion

        public DatabaseObject()
        {
            var a = 5;
        }

        #region Properties

        [Column(nameof(Id))]
        public int Id
        {
            get
            {
                if (id == null)
                {
                    throw new DatabaseIdException("No Id is set, because the object hasn't been added to the database.");
                }
                return id.Value;
            }
        }

        #endregion

        public void DeletedFromDatabase()
        {
            if (id == null)
            {
                throw new DatabaseIdException("This object hasn't been added to a database.");
            }

            id = null;
        }

        public bool HasId() => id.HasValue;

        public void InsertedInDatabase(int insertId)
        {
            if (id != null)
            {
                throw new DatabaseIdException("This object has already been added to a database.");
            }

            id = insertId;
        }

        public bool TryValidate()
        {
            var context = new ValidationContext(this, null, null);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(this, context, results, true);
            if (!valid)
            {
                Debug.WriteLine(results);
            }
            return valid;
        }
    }
}
