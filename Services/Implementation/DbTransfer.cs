using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;
using Services.DTO_Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class DbTransfer : IDbTransfer
    {
        public DbTransfer(ApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public ApplicationDbContext DbContext { get; }
        public IMapper Mapper { get; }

        public List<RegionVM> GetRegions()
        {
            var regions = DbContext.Regions.Include(x => x.Countries).Select(x => new
            {
                RegionName = x.RegionName,
                Countries = x.Countries.Select(z => z.CountryName).ToList()
            }
            ).ToList();
            var res = Mapper.Map<List<RegionVM>>(DbContext.Regions.ToList());
            return res;

        }

        public List<CountryVM> GetCountryByRegion(string id)
        {
            var res = DbContext.Countries.Where(x => x.RegionId == id).ToList();
            var mapped = Mapper.Map<List<CountryVM>>(res);
            return mapped;

        }

        public OrderVM SearchOrdersByItemType(string item)
        {
            var res = DbContext.Orders.FirstOrDefault(x => x.ItemType.ToLower() == item.ToLower());
            var mapped = Mapper.Map<OrderVM>(res);
            return mapped;
        }
    }
}
