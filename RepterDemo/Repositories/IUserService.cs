using RepterDemo.Models;

namespace RepterDemo.Repositories
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();



    }
}
