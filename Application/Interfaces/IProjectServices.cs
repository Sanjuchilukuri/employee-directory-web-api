using Application.DTO;

namespace Application.Interfaces
{
    public interface IProjectServices
    {
        public Task<List<CommonDTO>> GetProjectsAsync();
    }
}