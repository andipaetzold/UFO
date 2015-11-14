namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(nameof(User))]
    public class User : DatabaseObject
    {
        public User()
        {
        }

        public User(int id, string username, string password, string email, bool isAdmin)
            : this(username, password, email, isAdmin)
        {
            InsertedInDatabase(id);
        }

        public User(string username, string password, string email, bool isAdmin)
        {
            Username = username;
            Password = password;
            Email = email;
            IsAdmin = isAdmin;
        }

        #region Properties

        [StringLength(128)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [Required]
        [StringLength(64)]
        public string Username { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode() ^ Password.GetHashCode() ^ Email.GetHashCode() ^ IsAdmin.GetHashCode();
        }
    }
}
