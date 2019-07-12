using System.Collections.Generic;
using Warehouse.Data.Entities;

namespace Warehouse.Business.Services
{
    /// <summary>
    /// This service has common methods for yours all entity.
    /// </summary>
    /// <typeparam name="TEntity">You must enter database entity type.</typeparam>
    public interface IServiceBase<TEntity> where TEntity : EntityBase, new()
    {
        /// <summary>
        /// You can use this method for update already record in database.
        /// </summary>
        /// <param name="id">Enter you can want updating record id value.</param>
        /// <param name="entity">Entity object usage for update database record.</param>
        /// <returns></returns>
        TEntity Update(int id, TEntity entity);

        /// <summary>
        /// You can use this method for create new record in database.
        /// </summary>
        /// <param name="entity">Entity object usage for create database record.</param>
        /// <returns></returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// You can get just one data entered your id number with id parameter.
        /// </summary>
        /// <param name="id">Get just one data with entered your id number with id parameter.</param>
        /// <returns>Returned limited data with pagination features grid</returns>
        TEntity Get(int id);

        /// <summary>
        /// You can get data with entered skip and take number value thereby will take filtered data.
        /// </summary>
        /// <param name="skip">If you want skip data then you can skip data with enter number.</param>
        /// <param name="take">If want limited data then enter limit number.</param>
        /// <returns>Returned limited data with pagination features grid</returns>
        ICollection<TEntity> Get(int skip, int take);

        /// <summary>
        /// You can get data with entered skip and take number value thereby will take filtered data.
        /// </summary>
        /// <param name="skip">If you want skip data then you can skip data with enter number.</param>
        /// <param name="take">If want limited data then enter limit number.</param>
        /// <param name="orderProperty">If want ordered data then enter property name.</param>
        /// <param name="orderType">You can order type like as ASC or DESC.</param>
        /// <returns>Returned limited data with pagination features grid</returns>
        KeyValuePair<ICollection<TEntity>, int> GetWithCount(int skip, int take, string orderProperty = "", string orderType = "ASC");

        /// <summary>
        /// You can delete product with id value.
        /// </summary>
        /// <param name="id">You must be entered id value for you can delete product.</param>
        /// <returns></returns>
        bool Delete(int id);
    }
}
