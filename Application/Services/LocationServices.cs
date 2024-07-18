using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class LocationServices : ILocationServices
    {
        private readonly ILocationRepo _locationRepo;

        private readonly IMapper _mapper;

        public LocationServices(ILocationRepo locationRepo, IMapper mapper)
        {
            _locationRepo = locationRepo;
            _mapper = mapper;
        }

        public async Task<List<CommonDTO>> GetLoacationsAsync()
        {
            List<CommonDTO> locations = new List<CommonDTO>();
            foreach (var location in await _locationRepo.GetLocationsAsync())
            {
                locations.Add(_mapper.Map<CommonDTO>(location));
            }
            return locations;
        }
    }
}