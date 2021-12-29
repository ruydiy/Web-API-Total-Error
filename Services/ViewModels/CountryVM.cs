using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO_Models
{
    public class CountryVM
    {
        public string CountryName { get; set; }
        public string RegionId { get; set; }
        public RegionVM Region { get; set; }

        public List<OrderVM> Orders { get; set; }
    }
}
