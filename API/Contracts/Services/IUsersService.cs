using API.Models.DB;

namespace API.Contracts.Services
{
    public interface IUsersService
    {
        User GetUserById(int id);

        IList<User> GetUsers();

        bool CreateUser(User user);
    }
}
