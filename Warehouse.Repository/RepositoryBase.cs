using System.Collections.Generic;
using Warehouse.Business.Services;
using Warehouse.Data.Entities;

namespace Warehouse.Repository
{
    /// <summary>
    /// This repository has common methods for yours all entity.
    /// </summary>
    /// <typeparam name="TEntity">You must enter database entity type.</typeparam>
    public class RepositoryBase<TEntity> where TEntity : EntityBase, new()
    {
        private readonly ServiceBase<TEntity> _serviceBase;

        public RepositoryBase(ServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        /// <summary>
        /// You can get data with entered skip and take number value thereby will take filtered data.
        /// </summary>
        /// <param name="skip">If you want skip data then you can skip data with enter number.</param>
        /// <param name="take"></param>
        /// <returns></returns>
        public ICollection<TEntity> GetAll(int skip, int take)
            => _serviceBase.Get(skip, take);
    }
}
