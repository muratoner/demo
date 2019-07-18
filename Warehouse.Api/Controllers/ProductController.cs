using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Business.Services;
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
            [FromQuery(Name = "_order")]string orderType)
                => ToResultData(_serviceBase.GetWithCount, skip, take, orderProperty, orderType);

        /// <summary>
        /// You can get products from your sent id parameter.
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
            => ToResultModel(_serviceBase.Create, product, _validatorProduct, product);

        /// <summary>
        /// You can update product.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        [HttpPut("{id}")]
        public ResultModel<Product> Put(int id, [FromBody] Product product)
            => ToResultModel(_serviceBase.Update, id, product, _validatorProduct, product);

        /// <summary>
        /// You can delete product with id parameter.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public Result Delete(int id)
            => ToResult(_serviceBase.Delete, id);
    }
}
