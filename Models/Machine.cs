using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SodaMachine.Models
{
    public class Machine
    {
        public int Id { get; set; }

        public List<Soda> Sodas { get; set; }

        public int Money { get; set; }
    }
}
