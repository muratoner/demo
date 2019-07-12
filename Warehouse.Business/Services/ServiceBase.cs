using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Warehouse.Data;
using Warehouse.Data.Entities;

namespace Warehouse.Business.Services
{
    /// <summary>
    /// This service has common methods for yours all entity.
    /// </summary>
    /// <typeparam name="TEntity">You must enter database entity type.</typeparam>
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : EntityBase, new()
    {
        protected WarehouseDbContext WarehouseDbContext;
        protected IHttpContextAccessor HttpContextAccessor;

        protected ServiceBase(WarehouseDbContext warehouseDbContext, IHttpContextAccessor httpContextAccessor)
        {
            WarehouseDbContext = warehouseDbContext;
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// You can use this method for update already record in database.
        /// </summary>
        /// <param name="id">Enter you can want updating record id value.</param>
        /// <param name="entity">Entity object usage for update database record.</param>
        /// <returns></returns>
        public abstract TEntity Update(int id, TEntity entity);

        /// <summary>
        /// You can use this method for create new record in database.
        /// </summary>
        /// <param name="entity">Entity object usage for create database record.</param>
        /// <returns></returns>
        public TEntity Create(TEntity entity)
        {
            WarehouseDbContext.Set<TEntity>().Add(entity);
            WarehouseDbContext.SaveChanges();
            return entity;
        }

        /// <summary>
        /// You can get just one data entered your id number with id parameter.
        /// </summary>
        /// <param name="id">Get just one data with entered your id number with id parameter.</param>
        /// <returns>Returned limited data with pagination features grid</returns>
        public TEntity Get(int id)
            => WarehouseDbContext.Set<TEntity>().FirstOrDefault(p => p.Id == id);

        /// <summary>
        /// You can get data with entered skip and take number value thereby will take filtered data.
        /// </summary>
        /// <param name="skip">If you want skip data then you can skip data with enter number.</param>
        /// <param name="take">If want limited data then enter limit number.</param>
        /// <returns>Returned limited data with pagination features grid</returns>
        public ICollection<TEntity> Get(int skip, int take)
            => GetWithCount(skip, take).Key;

        /// <summary>
        /// You can get data with entered skip and take number value thereby will take filtered data.
        /// </summary>
        /// <param name="skip">If you want skip data then you can skip data with enter number.</param>
        /// <param name="take">If want limited data then enter limit number.</param>
        /// <param name="orderProperty">If want ordered data then enter property name.</param>
        /// <param name="orderType">You can order type like as ASC or DESC.</param>
        /// <returns>Returned limited data with pagination features grid with all data count.</returns>
        public KeyValuePair<ICollection<TEntity>, int> GetWithCount(
            int skip,
            int take,
            string orderProperty = "",
            string orderType = "ASC")
        {
            var filters = GetFilters();

            var entity = WarehouseDbContext.Set<TEntity>();
            var expression = entity.AsQueryable();
            if (!string.IsNullOrEmpty(orderProperty))
                expression = expression.OrderBy($"{orderProperty} {orderType}");
            foreach (var (key, value) in filters)
                expression = expression.Where($"{key} = @0", value);
            return new KeyValuePair<ICollection<TEntity>, int>(expression.Skip(skip == 1 ? 0 : (skip - 1) * take).Take(take).ToList(), expression.Count());
        }

        /// <summary>
        /// You can delete product with id value.
        /// </summary>
        /// <param name="id">You must be entered id value for you can delete product.</param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var product = WarehouseDbContext.Product.FirstOrDefault(p => p.Id == id);
            if (product == null) return default(bool);
            WarehouseDbContext.Product.Remove(product);
            return WarehouseDbContext.SaveChanges() > 0;
        }

        private Dictionary<string, string> GetFilters()
        {
            var query = HttpContextAccessor.HttpContext.Request.Query;
            return query
                .Where(item => item.Key.EndsWith("_like"))
                .ToDictionary<KeyValuePair<string, StringValues>, string, string>(item => item.Key.Replace("_like", ""), item => item.Value);
        }
    }
}
