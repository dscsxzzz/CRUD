using TestCRUD.Models;

namespace TestCRUD.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<User> UpdateUser(int id, User user);
        Task<User> AddUser(User user);
        Task<User> DeleteUser(int id);
    }
}
