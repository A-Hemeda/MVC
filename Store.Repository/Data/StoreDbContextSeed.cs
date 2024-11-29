using Store.Core.Entities;
using Store.Core.Entities.Order;
using Store.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository.Data
{
    public class StoreDbContextSeed
    {

        public async static Task SeedAsync(StoreDbContext _context)
        {
            
            if(_context.Brands.Count() == 0)
            {
                // Brand 
                // 1. Read data from json file 
                var brandsData = File.ReadAllText(@"..\Store.Repository\Data\DataSeed\Brands.json");
                // C:\Users\HESHAM\source\repos\Store\Store.Repository\Data\DataSeed\Brands.json

                // Cponvert Json string To List<T>
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                // Seed Data to database
                if (brandsData is not null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }



            if (_context.Types.Count() == 0)
            {
                // Brand 
                // 1. Read data from json file 
                var TypesData = File.ReadAllText(@"..\Store.Repository\Data\DataSeed\Types.json");
                // C:\Users\HESHAM\source\repos\Store\Store.Repository\Data\DataSeed\Brands.json

                // Cponvert Json string To List<T>
                var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                // Seed Data to database
                if (TypesData is not null && types.Count() > 0)
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }
            }



            if(_context.Products.Count() == 0)
            {
                // Brand 
                // 1. Read data from json file 
                var ProductData = File.ReadAllText(@"..\Store.Repository\Data\DataSeed\Products.json");
                // @"..\Store\Store.Repository\Data\DataSeed\Brands.json"

                // Cponvert Json string To List<T>
                var product = JsonSerializer.Deserialize<List<Product>>(ProductData);

                // Seed Data to database
                if (ProductData is not null && product.Count() > 0)
                {
                    await _context.Products.AddRangeAsync(product);
                    await _context.SaveChangesAsync();
                }
            }


            if (_context.DeliveryMethods.Count() == 0)
            {
                // delivery methods 
                // 1. Read data from json file 
                var deliveryData = File.ReadAllText(@"..\Store.Repository\Data\DataSeed\delivery.json");
                // @"..\Store\Store.Repository\Data\DataSeed\Brands.json"

                // Cponvert Json string To List<T>
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                // Seed Data to database
                if (deliveryMethods is not null)
                {
                    await _context.DeliveryMethods.AddRangeAsync(deliveryMethods);
                    await _context.SaveChangesAsync();
                }
            }

        }


    }
}
