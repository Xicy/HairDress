using System;
using System.Collections.Generic;
using System.Linq;

namespace HairDress.VOL
{
    public class Operation2Person
    {
        public int ID { set; get; }
        public DateTime Date { set; get; }
        public List<Operation> Operations { set; get; }
        public double TotalCost => Operations.Select(x => x.Price).Sum();
    }
}