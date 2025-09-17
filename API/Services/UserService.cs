using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Domain;
using API.Data.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _manager;
        private readonly DataContext _context;

        public UserService(DataContext context, UserManager<IdentityUser> manager)
        {
            _context = context;
            _manager = manager;
        }
        public async Task<UserDto> CreateUserAsync(CreateUserDto createDto)
        {
            if (createDto != null)
            {
                var user = new User
                {
                    UserName = createDto.Email,
                    Email = createDto.Email,
                };

                var result = await _manager.CreateAsync(user, createDto.Password);

                if (result.Succeeded)
                {
                    return new UserDto
                    {
                        Id = user.Id,
                        Email = createDto.Email,
                    };
                }
            }
            return null;
        }

        public async Task<UserDto> DeleteUserAsync(string userId)
        {
            var user = await _manager.FindByIdAsync(userId);
            if (user == null)
                return null;

            var result = await _manager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Email = user.Email
                };
            }
            return null;
        }

        public async Task<UserDto> GetCurrentUser(string email)
        {
            var user = await _manager.Users.FirstOrDefaultAsync(i => i.Email == email);
            if (user == null)
            {
                return null;
            }
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        public Task<UserDto> GetUserById(string userId)
        {
            throw new NotImplementedException();
        }
    }
}