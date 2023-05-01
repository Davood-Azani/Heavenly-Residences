using System.Threading.Tasks;
using Models;

namespace Heavenly_Residences_Client.Service.IService
{
    public interface IAuthenticationService
    //According to Api we wrote this methods
    {
        Task<RegisterationResponseDTO> RegisterUser(UserRequestDTO userForRegisteration);

        Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication);

        Task Logout();
    }
}
