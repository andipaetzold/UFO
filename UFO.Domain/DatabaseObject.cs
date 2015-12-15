namespace UFO.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using PropertyChanged;

    [ImplementPropertyChanged]
    public abstract class DatabaseObject : IDataErrorInfo,
                                           INotifyPropertyChanged
    {
        #region Fields

        [Range(1, int.MaxValue)]
        private int? id;

        #endregion

        #region Properties

        [DependsOn(nameof(Id))]
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

        #region IDataErrorInfo Members

        public string this[string porpertyName]
            => ValidateProperty(GetType().GetProperty(porpertyName).GetValue(this, null), porpertyName);

        public string Error => string.Empty;

        #endregion

        #region INotifyPropertyChanged Members
        

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

        protected string ValidateProperty(object value, string propertyName)
        {
            var info = GetType().GetProperty(propertyName);
            IEnumerable<string> errors =
                (info.GetCustomAttributes(true)
                     .OfType<ValidationAttribute>()
                     .Where(va => !va.IsValid(value))
                     .Select(va => va.FormatErrorMessage(string.Empty))).ToList();

            if (errors.Any())
            {
                Debug.WriteLine(errors.First());
                return errors.First();
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
