using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Domain
{
    public class Suspect
    {
        public int Id { get; set; }
        public Guid Link { get; set; } = Guid.NewGuid();
        required public string Name { get; set; }
        public int AmountOfCrime { get; set; }
        required public string Address { get; set; }
    }
}