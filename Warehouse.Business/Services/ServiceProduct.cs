using Microsoft.AspNetCore.Http;
using System.Linq;
using Warehouse.Data;
using Warehouse.Data.Entities;

namespace Warehouse.Business.Services
{
    public class ServiceProduct : ServiceBase<Product>
    {
        public ServiceProduct(
            WarehouseDbContext warehouseDbContext, 
            IHttpContextAccessor httpContextAccessor) 
                : base(warehouseDbContext, httpContextAccessor)
        {

        }

        public override Product Update(int id, Product entity)
        {
            var dbEntity = WarehouseDbContext.Product.FirstOrDefault(p => p.Id == id);
            if (dbEntity == null) return entity;

            dbEntity.Category = entity.Category;
            dbEntity.Color = entity.Color;
            dbEntity.Company = entity.Company;
            dbEntity.Currency = entity.Currency;
            dbEntity.Description = entity.Description;
            dbEntity.Ean13 = entity.Ean13;
            dbEntity.Mail = entity.Mail;
            dbEntity.Name = entity.Name;
            dbEntity.Price = entity.Price;
            dbEntity.Review = entity.Review;
            dbEntity.Seller = entity.Seller;

            WarehouseDbContext.SaveChanges();
            return entity;
        }
    }
}
