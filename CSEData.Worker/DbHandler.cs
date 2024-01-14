using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CSEData.Worker
{
    public class DbHandler
    {
        private readonly IStockDbContext _db;


        public DbHandler(IStockDbContext db)
        {
            _db = db;

        }
        public void DataHandler(List<Dictionary<string, string>> scrapedDataList)
        {
            foreach (var scrapedData in scrapedDataList)
            {
                if (scrapedData != null)
                {
                    var companyId = Convert.ToInt32(scrapedData["SL"]);

                    var company = _db.Company.FirstOrDefault(x => x.CompantyName == scrapedData["StockCode"]);
                    if (company != null)
                    {
                        var stocks = _db.StockPrice.Where(b => b.CompanyId == company.Id).ToList();
                        var newStock = new StockPrice
                        {
                            CompanyId = company.Id,
                            Price = Convert.ToDouble(scrapedData["LTP"]),
                            Volume = Convert.ToDouble(scrapedData["Volume"]),
                            Open = Convert.ToDouble(scrapedData["Open"]),
                            High = Convert.ToDouble(scrapedData["High"]),
                            Low = Convert.ToDouble(scrapedData["Low"]),
                            Time = DateTime.Now,


                        };
                        stocks.Add(newStock);
                        company.Stocks = stocks;
                        _db.SaveChanges();
                    }
                    else
                    {
                        var newCompany = new Company
                        {
                            //Id = companyId,
                            CompantyName = scrapedData["StockCode"]
                        };

                        var newStock = new StockPrice
                        {
                            //CompanyId = company.Id,
                            Price = Convert.ToDouble(scrapedData["LTP"]),
                            Volume = Convert.ToDouble(scrapedData["Volume"]),
                            Open = Convert.ToDouble(scrapedData["Open"]),
                            High = Convert.ToDouble(scrapedData["High"]),
                            Low = Convert.ToDouble(scrapedData["Low"]),
                            Time = DateTime.Now,


                        };
                        newCompany.Stocks = new List<StockPrice>();
                        newCompany.Stocks.Add(newStock);
                        _db.Company.Add(newCompany);

                        _db.SaveChanges();



                    }
                }
            }


        }
    }

}
