using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IProjectRepo
    {
        public Task<List<Project>> GetProjectsAsync();
    }
}