using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Warehouse.Business;
using Warehouse.Business.Services;
using Warehouse.Core.Extensions;
using Warehouse.Data.Entities;
using Warehouse.Poco.Result;

namespace Warehouse.Api.Controllers
{
    /// <summary>
    /// You can do it Get, Add, Update and Delete operations with in this controller methods for product.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IServiceBase<Product> _serviceBase;
        private readonly IValidator<Product> _validatorProduct;

        public ProductController(IServiceBase<Product> serviceBase, IValidator<Product> validatorProduct)
        {
            _serviceBase = serviceBase;
            _validatorProduct = validatorProduct;
        }

        /// <summary>
        /// You can get products from default company warehouse.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultGrid<Product> Get(
            [FromQuery(Name = "_page")]int skip, 
            [FromQuery(Name = "_limit")]int take, 
            [FromQuery(Name = "_sort")]string orderProperty, 
            [FromQuery(Name = "_order")]string orderType,
            [FromQuery(Name = "_filter_property_name")]string filterProperty)
            => ToResultData(_serviceBase.GetWithCount, skip, take, orderProperty, orderType);

        /// <summary>
        /// You can get products from your sended id parameter.
        /// </summary>
        /// <param name="id">You can get just one data with entered your id number value.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ResultModel<Product> Get(int id)
            => ToResultModel(_serviceBase.Get, id);

        /// <summary>
        /// You can add new product.
        /// </summary>
        /// <param name="product">Product entry.</param>
        [HttpPost]
        public ResultModel<Product> Post([FromBody] Product product)
        {
            var res = new ResultModel<Product>();
            var resValidation = _validatorProduct.Validate(product);
            if (!resValidation.IsValid)
            {
                resValidation.Errors.Select(e => e.ErrorMessage).ForEach(res.Errors.Add);
            }
            if(res.HasError)
                return res;
            res.Model = _serviceBase.Create(product);
            return res;
        }

        /// <summary>
        /// You can update product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        [HttpPut("{id}")]
        public ResultModel<Product> Put(int id, [FromBody] Product product)
        {
            var res = new ResultModel<Product>();
            var resValidation = _validatorProduct.Validate(product);
            if (!resValidation.IsValid)
                resValidation.Errors.Select(e => e.ErrorMessage).ForEach(res.Errors.Add);
            
            if (res.HasError)
                return res;

            res.Model = _serviceBase.Update(id, product);
            return res;
        }

        /// <summary>
        /// You can delete product with id parameter.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public Result Delete(int id)
            => ToResult(_serviceBase.Delete, id);
    }
}
