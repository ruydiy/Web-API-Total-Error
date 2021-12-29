using Services.DTO_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDbTransfer
    {
        public List<RegionVM> GetRegions();
        public List<CountryVM> GetCountryByRegion(string id);
        public OrderVM SearchOrdersByItemType(string item);
    }
}
