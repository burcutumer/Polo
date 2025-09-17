using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Domain;
using API.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuspectController : ControllerBase
    {

        private readonly ILogger<SuspectController> _logger;
        public DataContext _context { get; }

        public SuspectController(ILogger<SuspectController> logger, DataContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Suspect>>> GetSuspectList()
        {
            var result = await _context.Suspects.ToListAsync();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Could not load the Usual Suspects");
            }

        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Suspect>> AddSuspect(CreateSuspectDto dto)
        {
            var sus = new Suspect
            {
                Link = Guid.NewGuid(),
                Name = dto.Name,
                Address = dto.Address,
                AmountOfCrime = dto.AmountOfCrime
            };

            await _context.Suspects.AddAsync(sus);
            var saved = await _context.SaveChangesAsync();

            if (saved > 0)
            {
                return Ok(sus);
            }
            else
            {
                return BadRequest("Could not save Suspect");
            }

        }

    }
}
