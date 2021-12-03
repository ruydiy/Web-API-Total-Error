using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Base
    {
        public Base()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
    }
}
