using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public interface  IStockDbContext
    {
        DbSet<StockPrice> StockPrice { get; set; }
        DbSet<Company> Company { get; set; }

        int SaveChanges();
    }
}
