using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class ProjectServices : IProjectServices
    {
        private readonly IProjectRepo _projectRepo;

        private readonly IMapper _mapper;

        public ProjectServices(IProjectRepo projectRepo, IMapper mapper)
        {
            _projectRepo = projectRepo;
            _mapper = mapper;
        }

        public async Task<List<CommonDTO>> GetProjectsAsync()
        {
            List<CommonDTO> locations = new List<CommonDTO>();
            foreach (var project in await _projectRepo.GetProjectsAsync())
            {
                locations.Add(_mapper.Map<CommonDTO>(project));
            }
            return locations;
        }
    }
}