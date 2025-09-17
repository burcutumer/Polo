using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Dtos;

namespace API.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserDto createDto);
        Task<UserDto> DeleteUserAsync(string userId);
        Task<UserDto> GetUserById(string userId);
        Task<UserDto> GetCurrentUser(string email);
    }
}