using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Country : Base
    {
        public Country() : base()
        {

        }

        public string CountryName { get; set; }
        public string RegionId { get; set; }
        public Region Region { get; set; }
    }
}
