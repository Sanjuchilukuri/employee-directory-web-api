using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface ILocationRepo
    {
        public  Task<List<Location>> GetLocationsAsync();
    }
}