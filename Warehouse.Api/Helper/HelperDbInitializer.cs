using Bogus.DataSets;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Warehouse.Data;
using Warehouse.Data.Entities;

namespace Warehouse.Api.Helper
{
    public class HelperDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<WarehouseDbContext>();
                if (context.Product.Any()) return;
                var commerce = new Commerce();
                var finance = new Finance();
                var lorem = new Lorem();
                var company = new Company();
                var internet = new Internet();
                var name = new Name();
                var rant = new Rant();

                for (var i = 0; i < 100000; i++)
                {
                    context.Product.Add(new Product
                    {
                        Category = commerce.Categories(1)[0],
                        Color = commerce.Color(),
                        Ean13 = commerce.Ean13(),
                        Name = commerce.ProductName(),
                        Price = commerce.Price(),
                        Currency = finance.Currency().Code,
                        Description = lorem.Sentences(),
                        Company = company.CompanyName(),
                        Mail = internet.Email(),
                        Seller = name.FullName(),
                        Review = rant.Review()
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
