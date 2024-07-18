using Application.DTO.AuthModels;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using BCrypt.Net;
using Infrastructure.Entities;
using Infrastructure.Interfaces;

namespace Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepo _userRepo;

        private readonly IMapper _mapper;

        public UserServices(IUserRepo userRepo, IMapper mapper)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public async Task<UserDTO> ValidateUser(LoginModel user)
        {
            var loggedInUser =  _mapper.Map<UserDTO>(await _userRepo.ValidateLogin(_mapper.Map<User>(user)) );

            if (loggedInUser != null && BCrypt.Net.BCrypt.Verify(user.Password,loggedInUser.Password))
            {
                return loggedInUser;
            }
            return null!;
        }

        public async Task<bool> RegisterUser(RegisterModel newUser)
        {
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            return await _userRepo.RegisterUser(_mapper.Map<User>(newUser));
        }
    }
} 