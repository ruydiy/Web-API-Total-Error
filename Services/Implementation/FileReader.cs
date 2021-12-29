using CsvHelper;
using Data;
using Models;
using Services.DTO_Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class FileReader : IFileReader
    {
        public FileReader(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; }

        public List<TransferModel> ReadFileFromDirectory(string dir)
        {
            string[] files = System.IO.Directory.GetFiles(dir);


            List<TransferModel> models = new List<TransferModel>();
            var lastReadedDate = DbContext.LastReadedFiles.OrderByDescending(x => x.LastReaded);

            foreach (string currentFile in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(currentFile);
                if (lastReadedDate.Count() > 0)
                {
                    if (lastReadedDate.First().LastReaded < DateTime.Parse(fileName))
                    {
                        using (var reader = new StreamReader(currentFile))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            models = csv.GetRecords<TransferModel>().ToList();
                        }

                        Region region;
                        for (int i = 0; i < models.Count; i++)
                        {
                            region = DbContext.Regions.FirstOrDefault(x => x.RegionName.ToUpper() == models[i].Region.ToUpper());

                            if (region != null)
                            {
                                if (region.Countries.Any(x => x.CountryName.ToUpper() == models[i].Country.ToUpper()))
                                {
                                    Country country = region.Countries.FirstOrDefault(x => x.CountryName == models[i].Country);
                                    Order newOrd = new Order()
                                    {
                                        ItemType = models[i].ItemType,
                                        SalesChannel = models[i].SalesChannel,
                                        OrderPrioriy = models[i].OrderPrioriy,
                                        OrderDate = models[i].OrderDate,
                                        OrderId = models[i].OrderId,

                                    };
                                    DbContext.Orders.Add(newOrd);
                                    Sale newSale = new Sale()
                                    {
                                        ShipDate = models[i].ShipDate,
                                        UnitsSold = models[i].UnitsSold,
                                        UnitPrice = models[i].UnitPrice,
                                        UnitCost = models[i].UnitCost,
                                        TotalRevenue = models[i].TotalRevenue,
                                        TotalCost = models[i].TotalCost,
                                        TotalProfit = models[i].TotalProfit,
                                    };
                                    DbContext.Sales.Add(newSale);

                                    // DbContext.Countries.Update(country);
                                }
                                else
                                {
                                    Country newCountry = new Country()
                                    {
                                        CountryName = models[i].Country,
                                        RegionId = region.Id,
                                        Orders = new List<Order>() { new Order()
                                        {
                                            ItemType = models[i].ItemType,
                                            SalesChannel = models[i].SalesChannel,
                                            OrderPrioriy = models[i].OrderPrioriy,
                                            OrderDate = models[i].OrderDate,
                                            OrderId = models[i].OrderId,
                                        }
                                        }
                                    };
                                    DbContext.Countries.Add(newCountry);
                                }

                            }
                            else
                            {
                                Region newReg = new Region()
                                {
                                    RegionName = models[i].Region,
                                    Countries = new List<Country>()
                                    {
                                        new Country()
                                        {
                                            CountryName = models[i].Country,
                                            Orders = new List<Order>()
                                            {
                                                new Order()
                                                {
                                                    ItemType = models[i].ItemType,
                                                    SalesChannel = models[i].SalesChannel,
                                                    OrderPrioriy = models[i].OrderPrioriy,
                                                    OrderDate = models[i].OrderDate,
                                                    OrderId = models[i].OrderId,
                                                    Sales = new List<Sale>()
                                                    {
                                                        new Sale()
                                                        {
                                                            ShipDate = models[i].ShipDate,
                                                            UnitsSold = models[i].UnitsSold,
                                                            UnitPrice = models[i].UnitPrice,
                                                            UnitCost = models[i].UnitCost,
                                                            TotalRevenue = models[i].TotalRevenue,
                                                            TotalCost = models[i].TotalCost,
                                                            TotalProfit = models[i].TotalProfit,
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                };
                                DbContext.Regions.Add(newReg);
                            }
                        }
                    }
                    DbContext.LastReadedFiles.Add(new LastReadedFile() { LastReaded = DateTime.Parse(fileName) });
                }
                else
                {
                    using (var reader = new StreamReader(currentFile))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        models = csv.GetRecords<TransferModel>().ToList();
                    }
                    List<Region> regions = new List<Region>();

                    for (int i = 0; i < models.Count; i++)
                    {
                        Region currentRegion = regions.FirstOrDefault(x => x.RegionName.ToUpper() == models[i].Region.ToUpper());
                        if (currentRegion != null)
                        {
                            Country currentCountry = currentRegion.Countries.FirstOrDefault(x => x.CountryName.ToUpper() == models[i].Country.ToUpper());

                            if (currentCountry != null)
                            {
                                currentCountry.Orders.Add(new Order()
                                {
                                    ItemType = models[i].ItemType,
                                    SalesChannel = models[i].SalesChannel,
                                    OrderPrioriy = models[i].OrderPrioriy,
                                    OrderDate = models[i].OrderDate,
                                    OrderId = models[i].OrderId,

                                });

                                Order currentOrder = currentCountry.Orders.FirstOrDefault(x => x.ItemType.ToUpper() == models[i].ItemType.ToUpper());
                                if (currentOrder != null)
                                {
                                    currentOrder.Sales.Add(new Sale()
                                    {
                                        ShipDate = models[i].ShipDate,
                                        UnitsSold = models[i].UnitsSold,
                                        UnitPrice = models[i].UnitPrice,
                                        UnitCost = models[i].UnitCost,
                                        TotalRevenue = models[i].TotalRevenue,
                                        TotalCost = models[i].TotalCost,
                                        TotalProfit = models[i].TotalProfit,
                                    });
                                }
                            }
                            else
                            {
                                currentRegion.Countries.Add(new Country()
                                {
                                    CountryName = models[i].Country,
                                    Orders = new List<Order>() {new Order()
                                    {
                                            ItemType = models[i].ItemType,
                                            SalesChannel = models[i].SalesChannel,
                                            OrderPrioriy = models[i].OrderPrioriy,
                                            OrderDate = models[i].OrderDate,
                                            OrderId = models[i].OrderId,
                                            Sales = new List<Sale>()
                                            {
                                                new Sale()
                                                {
                                                   ShipDate = models[i].ShipDate,
                                                   UnitsSold = models[i].UnitsSold,
                                                   UnitPrice = models[i].UnitPrice,
                                                   UnitCost = models[i].UnitCost,
                                                   TotalRevenue = models[i].TotalRevenue,
                                                   TotalCost = models[i].TotalCost,
                                                   TotalProfit = models[i].TotalProfit,
                                                }
                                            }

                                    }

                                    }


                                });
                            }

                        }
                        else
                        {
                            regions.Add(new Region()
                            {
                                RegionName = models[i].Region,
                                Countries = new List<Country>()
                                    {
                                        new Country()
                                        {
                                            CountryName=models[i].Country,
                                            Orders = new List<Order>()
                                            {
                                                new Order()
                                                {
                                                    ItemType = models[i].ItemType,
                                                    SalesChannel = models[i].SalesChannel,
                                                    OrderPrioriy = models[i].OrderPrioriy,
                                                    OrderDate = models[i].OrderDate,
                                                    OrderId = models[i].OrderId,
                                                    Sales = new List<Sale>()
                                                    {
                                                        new Sale()
                                                        {
                                                            ShipDate = models[i].ShipDate,
                                                            UnitsSold = models[i].UnitsSold,
                                                            UnitPrice = models[i].UnitPrice,
                                                            UnitCost = models[i].UnitCost,
                                                            TotalRevenue = models[i].TotalRevenue,
                                                            TotalCost = models[i].TotalCost,
                                                            TotalProfit = models[i].TotalProfit,
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                            });
                        }
                    }
                    DbContext.Regions.AddRange(regions);
                    DbContext.LastReadedFiles.Add(new LastReadedFile() { LastReaded = DateTime.Parse(fileName) });
                    DbContext.SaveChanges();
                }

                DbContext.SaveChanges();
            }
            return models;
        }
    }
}
