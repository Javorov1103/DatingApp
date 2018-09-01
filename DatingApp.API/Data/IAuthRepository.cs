namespace DatingApp.API.Data
{
    using System.Threading.Tasks;
    using DatingApp.API.Models;

    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);

         Task<User> Login (string username, string password);

         Task<bool> UserExist(string username);
    }
}