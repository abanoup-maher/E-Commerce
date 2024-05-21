using Microsoft.Extensions.Logging;
using Rocket.Core.Entities;
using Rocket.Core.Entities.Identity.OrderAgreggate;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rocket.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(RocketStoreDbContext context , ILoggerFactory logger)
        {
            ////
            try
            {
                if (!context.ProductBrands.Any()) 
                {
                    var brandsdata = File.ReadAllText("../Rocket.Repository/Data/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);
                    foreach (var brand in brands)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }
                }


                ////
                if (!context.ProductTypes.Any())
                {
                    var Typesdata = File.ReadAllText("../Rocket.Repository/Data/DataSeed/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(Typesdata);
                    foreach (var type in types)
                    {
                        context.Set<ProductType>().Add(type);
                    }
                }


                ///////
                if (!context.Products.Any())
                {
                    var Productsdata = File.ReadAllText("../Rocket.Repository/Data/DataSeed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(Productsdata);
                    foreach (var product in products)
                    {
                        context.Set<Product>().Add(product);
                    }
                }
                // badefha fel database
                if (!context.DeliveryMethods.Any())
                {
                    var DeliveryMethod = File.ReadAllText("../Rocket.Repository/Data/DataSeed/delivery.json");
                    var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethod);
                    foreach (var d in delivery)
                    {
                        context.Set<DeliveryMethod>().Add(d);
                    }
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var log=logger.CreateLogger<StoreContextSeed>();
                log.LogError(ex,ex.Message);
            }
            
        }
    }
}
