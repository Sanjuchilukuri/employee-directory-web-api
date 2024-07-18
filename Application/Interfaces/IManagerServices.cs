using Application.DTO;

namespace Application.Interfaces
{
    public interface IManagerServices
    {
        public Task<List<CommonDTO>> GetMangersAsync();
    }
}