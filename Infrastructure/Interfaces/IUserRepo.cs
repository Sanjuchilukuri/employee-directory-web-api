using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserRepo
    {
        public Task<bool> RegisterUser(User user);
        public Task<User?> ValidateLogin(User user);
    }
}