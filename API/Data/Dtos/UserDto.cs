using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        required public string Email { get; set; }
    }
}