using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Dtos
{
    public class SuspectListDto
    {
        public int Id { get; set; }
        required public string Link { get; set; }
        required public string Name { get; set; }
        public int AmountOfCrime { get; set; }
        required public string Address { get; set; }
    }
}