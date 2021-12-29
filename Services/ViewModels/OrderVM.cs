using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO_Models
{
    public class OrderVM
    {
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public char OrderPrioriy { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public string CountryId { get; set; }
        public CountryVM Country { get; set; }
        public List<SaleVM> Sales { get; set; }
    }
}
