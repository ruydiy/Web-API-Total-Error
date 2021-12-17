using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO_Models
{
    public class RegionDTO
    {
        public string RegionName { get; set; }
        public List<CountryDTO> Countries { get; set; }
    }
}
