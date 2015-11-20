namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public class User : DatabaseObject
    {
        public User()
        {
        }

        public User(int id, string username, string password, string email)
            : this(username, password, email)
        {
            Id = id;
        }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        #region Properties

        [Column("Email")]
        [StringLength(128)]
        [EmailAddress]
        public string Email { get; set; }

        [Column("Password")]
        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [Column("Username")]
        [Required]
        [StringLength(64)]
        public string Username { get; set; }

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
            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Email?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Password?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Username?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        protected bool Equals(User other)
        {
            return string.Equals(Email, other.Email) && string.Equals(Password, other.Password)
                   && string.Equals(Username, other.Username);
        }
    }
}
