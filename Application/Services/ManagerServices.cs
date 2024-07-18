using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class ManagerServices : IManagerServices
    {
        private readonly IManagerRepo _managerRepo;

        private readonly IMapper _mapper;

        public ManagerServices(IManagerRepo managerRepo, IMapper mapper)
        {
            _managerRepo = managerRepo;
            _mapper = mapper;
        }

        public async Task<List<CommonDTO>> GetMangersAsync()
        {
            List<CommonDTO> locations = new List<CommonDTO>();
            foreach (var manager in await _managerRepo.GetManagersAsync())
            {
                locations.Add(_mapper.Map<CommonDTO>(manager));
            }
            return locations;
        }
    }
}