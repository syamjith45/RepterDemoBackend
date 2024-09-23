using RepterDemo.DTO;
using RepterDemo.Models;

namespace RepterDemo.Repositories
{
    public interface IAuthService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<RegistrationResponse> RegisterUserAsync(RegisterModel model);
        Task<LoginResult> LoginUserAsync(LoginModel model); // Updated return type
    }

}
