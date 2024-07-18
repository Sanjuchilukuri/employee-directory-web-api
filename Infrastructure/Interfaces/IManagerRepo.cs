using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IManagerRepo
    {
        public Task<List<Manager>> GetManagersAsync();
    }
}