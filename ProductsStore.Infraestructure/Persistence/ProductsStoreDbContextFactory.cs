using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsStore.Infraestructure.Persistence
{
    public class ProductsStoreDbContextFactory : IDesignTimeDbContextFactory<ProductsStoreDbContext>
    {
        public ProductsStoreDbContext CreateDbContext(string[] args)
        {
            //Necesario para aplicar las migraciones desde dotnet cli usando archivos de configuracion
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ProductsStoreApi"))
            .AddJsonFile("appsettings.json")
            .Build();

            DbContextOptionsBuilder<ProductsStoreDbContext> optionsBuilder = new();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _ = optionsBuilder.UseSqlServer(connectionString);

            return new ProductsStoreDbContext(optionsBuilder.Options);
        }
    }
}
