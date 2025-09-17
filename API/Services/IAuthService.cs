using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Dtos;
using Microsoft.AspNetCore.Identity.Data;

namespace API.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> CheckUserCredentials(LoginRequestDto loginRequest);
    }
}