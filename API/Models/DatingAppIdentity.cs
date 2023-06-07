namespace API.Models
{
    using API.Models.DB;
    using System.Security.Principal;
    public class DatingAppIdentity : GenericIdentity
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string ConnectionString { get; set; }

        public DatingAppIdentity(User user) :this (user.Id, user.Username)
        {
        }

        public DatingAppIdentity(int userId, string username) : base(username)
        {
            UserID = userId;
            Username = username;
        }
    }
}
