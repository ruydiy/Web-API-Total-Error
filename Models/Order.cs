using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order : Base
    {
        public Order() :base()
        {

        }
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public char OrderPrioriy { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public string CountryId { get; set; }
        public Country Country { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
