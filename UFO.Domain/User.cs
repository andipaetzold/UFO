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

        public User(int id, string username, string password, string email, bool isAdmin)
            : this(username, password, email, isAdmin)
        {
            Id = id;
        }

        public User(string username, string password, string email, bool isAdmin)
        {
            Username = username;
            Password = password;
            Email = email;
            IsAdmin = isAdmin;
        }

        #region Properties

        [Column("Email")]
        [StringLength(128)]
        [EmailAddress]
        public string Email { get; set; }

        [Column("IsAdmin")]
        [Required]
        public bool IsAdmin { get; set; }

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
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode() ^ Password.GetHashCode() ^ Email.GetHashCode() ^ IsAdmin.GetHashCode();
        }
    }
}
