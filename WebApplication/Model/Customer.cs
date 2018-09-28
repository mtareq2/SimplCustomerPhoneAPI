using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Model
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; } = String.Empty;
        public List<Phone> phones { get; set; } = new List<Phone>();
    }
}
