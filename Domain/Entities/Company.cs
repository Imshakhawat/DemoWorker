using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Company : IEntity<int>
    {
        public int Id { get; set; }
        public string CompantyName { get; set; }

        public List<StockPrice> Stocks { get; set; }
    }
}
