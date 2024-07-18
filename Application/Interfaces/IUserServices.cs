using Application.DTO;
using Application.DTO.AuthModels;

namespace Application.Interfaces
{
    public interface IUserServices
    {
        public Task<bool> RegisterUser(RegisterModel user);

        public Task<UserDTO> ValidateUser(LoginModel user);
    }
}