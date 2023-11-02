using Microsoft.AspNetCore.Identity;
using TestCRUD.Data;
using TestCRUD.Models;

namespace TestCRUD.Services
{
    public class UserService : IUserService
    {
       
        DataContext context;
        public UserService(DataContext context) {
            this.context = context;
        }

        public async Task<User> AddUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var Users = await context.Users.ToListAsync();
            return Users;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.email == email);
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await context.Users.FindAsync(id);
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.username == username);
            return user;
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            var existingUser = await context.Users.FindAsync(id);

            existingUser.id = existingUser.id;
            existingUser.name = user.name;
            existingUser.email = user.email;
            existingUser.password = user.password;
            existingUser.phone = user.phone;
            existingUser.username = user.username;
            
            await context.SaveChangesAsync();
            return user;
        }

        
    }
}
