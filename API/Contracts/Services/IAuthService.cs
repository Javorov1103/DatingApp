using API.Models;
using API.Models.DB;
using API.Models.DTO;

namespace API.Contracts
{
    public interface IAuthService
    {
        bool Register(RegisterUserDTO model);

        LoginResponseDTO Login(LoginUserDTO model);

        string CreateToken(LoginResponseDTO user);
    }
}
