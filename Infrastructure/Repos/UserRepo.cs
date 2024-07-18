using Infrastructure.DBContext;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly EmployeeDirectoryDbContext _dbContext;

        public UserRepo(EmployeeDirectoryDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> RegisterUser(User user)
        {
            await _dbContext.users.AddAsync(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<User?> ValidateLogin(User user)
        {
            return await _dbContext.users.SingleOrDefaultAsync(u => u.emailAddress == user.emailAddress);
        }

    }
}