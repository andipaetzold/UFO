namespace UFO.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics;

    public abstract class DatabaseObject
    {
        #region Fields

        [Range(1, int.MaxValue)]
        private int? id;

        #endregion
        

        #region Properties

        public bool HasId => id.HasValue;

        [Key]
        [Column("Id")]
        public int Id
        {
            get
            {
                if (id != null)
                {
                    return id.Value;
                }
                throw new InvalidOperationException("Object hasn't been added to the database, yet.");
            }
            set { id = value; }
        }

        #endregion

        public void DeleteId()
        {
            id = null;
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
