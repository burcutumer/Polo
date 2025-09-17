using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Dtos
{
    public class CreateUserDto
    {
        required public string Email { get; set; }
        required public string Password { get; set; }

    }
}