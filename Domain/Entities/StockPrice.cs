using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StockPrice : IEntity<int>
    {
       // (Id, CompanyId, Price (LTP), Volume, Open, High, Low, Time)
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public double Price { get; set; }
        public double Volume { get; set; }
        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public DateTime Time { get; set; }
    }
}
