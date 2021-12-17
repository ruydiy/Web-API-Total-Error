using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTO_Models
{
    public class OrderDTO
    {
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public char OrderPrioriy { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public string CountryId { get; set; }
        public CountryDTO country { get; set; }
        public List<SaleDTO> Sales { get; set; }
    }
}
