using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Data.DataSeeding
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DbInitializer(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task InitializeAsync()
        {
            try
            {
                if(_applicationDbContext.Database.GetPendingMigrations().Any())
                    {
                    await _applicationDbContext.Database.MigrateAsync();
                    if (!_applicationDbContext.ProductTypes.Any())
                    {
                        //..\Infrastructure\Persistance\Data\DataSeeding\types.json
                        var productTypes = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\types.json");
                        //Desirialization
                        var types = JsonSerializer.Deserialize<List<ProductType>>(productTypes);

                        if(types != null && types.Any())
                        {
                            await _applicationDbContext.AddRangeAsync(types);
                            await _applicationDbContext.SaveChangesAsync();
                        }
                    }
                    if (!_applicationDbContext.ProductBrands.Any())
                    {
                        //..\Infrastructure\Persistance\Data\DataSeeding\types.json
                        var productBrands = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\brands.json");
                        //Desirialization
                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrands);

                        if (brands != null && brands.Any())
                        {
                            await _applicationDbContext.AddRangeAsync(brands);
                            await _applicationDbContext.SaveChangesAsync();
                        }
                    }
                    if (!_applicationDbContext.Products.Any())
                    {
                        //..\Infrastructure\Persistance\Data\DataSeeding\types.json
                        var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\products.json");
                        //Desirialization
                        var product = JsonSerializer.Deserialize<List<Product>>(productData);

                        if (product != null && product.Any())
                        {
                            await _applicationDbContext.AddRangeAsync(product);
                            await _applicationDbContext.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
            }
        }
    }
   
}
