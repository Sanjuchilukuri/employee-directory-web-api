using Application.DTO;

namespace Application.Interfaces
{
    public interface ILocationServices
    {
        public Task<List<CommonDTO>> GetLoacationsAsync();
    }
}