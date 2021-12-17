using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO_Models
{
    public class CountryDTO
    {
        public string CountryName { get; set; }
        public string RegionId { get; set; }
        public RegionDTO Region { get; set; }
    }
}
