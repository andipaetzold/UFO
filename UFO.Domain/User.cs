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
            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode() ^ Password.GetHashCode() ^ Email.GetHashCode();
        }
    }
}
