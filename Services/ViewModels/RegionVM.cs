using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO_Models
{
    public class RegionVM
    {
        public string RegionName { get; set; }
        public List<CountryVM> Countries { get; set; }
    }
}
