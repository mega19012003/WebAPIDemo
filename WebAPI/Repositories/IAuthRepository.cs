using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(User user, string password);
        Task<User> LoginAsync(string username, string password);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(string username, string password, string fullname);
    }
}
