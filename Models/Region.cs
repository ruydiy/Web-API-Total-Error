using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Region : BaseModel
    {
        public Region() : base()
        {
             
        }

        public string RegionName { get; set; }
        public List<Country> Countries { get; set; }
    }
}
