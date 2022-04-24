using Core.Models;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText
                        (@"D:\ITI\Back End\Web API\E-Commerce Project\Infrastructure\Data\SeedData\brands.json");
                    var brands = JsonSerializer.Deserialize<IEnumerable<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                    {
                        context.ProductBrands.Add(brand);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText
                        (@"D:\ITI\Back End\Web API\E-Commerce Project\Infrastructure\Data\SeedData\types.json");
                    var types = JsonSerializer.Deserialize<IEnumerable<ProductType>>(typesData);
                    foreach (var type in types)
                    {
                        context.ProductTypes.Add(type);
                    }
                    await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    var productData = File.ReadAllText
                        (@"D:\ITI\Back End\Web API\E-Commerce Project\Infrastructure\Data\SeedData\products.json");
                    var products = JsonSerializer.Deserialize<IEnumerable<Product>>(productData);
                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}
