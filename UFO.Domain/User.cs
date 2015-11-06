namespace UFO.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class User : DatabaseObject
    {
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

        [StringLength(128)]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        [StringLength(32)]
        public string Password { get; set; }

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
