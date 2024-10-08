using System;
using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory){
        try{
#pragma warning disable CS8604 // Possible null reference argument.
            if (!context.productBrands.Any()){
                var brandsData =  File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                foreach(var brand in brands){
                    context.productBrands.Add(brand);
                }

                await context.SaveChangesAsync();
            }
#pragma warning restore CS8604 // Possible null reference argument.

            if (!context.ProductTypes.Any()){
                var typesData =  File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                foreach(var type in types){
                    context.ProductTypes.Add(type);
                }

                await context.SaveChangesAsync();
            }

            if(!context.Products.Any()){
                var productsData =  File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                foreach(var product in products){
                    context.Products.Add(product);
                }

                await context.SaveChangesAsync();
            }
        }
        catch(Exception ex){
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message);
        }

        
    }
}
