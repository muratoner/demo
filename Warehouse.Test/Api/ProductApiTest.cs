using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using FluentAssertions;
using Newtonsoft.Json;
using Warehouse.Core.Extensions;
using Warehouse.Data.Entities;
using Warehouse.Poco.Result;
using Xunit;

namespace Warehouse.Test.Api
{
    public class ProductApiTest
    {
        [Fact]
        public async Task Test_Get_Pagination_Not_Passed_Parameters()
        {
            using (var client = new TestClientProvider())
            {
                var response = await client.Client.GetAsync("/api/product");
                Common_Rules_For_HttpMessageMessage(response);

                var result = await response.ToModelAsync<ResultGrid<Product>>();

                Common_Rules_For_ResultGrid_Class(result, 0);

                result.Data.Count.Should().Be(0);
            }
        }

        [Fact]
        public async Task Test_Get_Pagination_Passed_Limit()
        {
            using (var client = new TestClientProvider())
            {
                const int requestCount = 10;
                var response = await client.Client.GetAsync($"/api/product?_limit={requestCount}");
                Common_Rules_For_HttpMessageMessage(response);

                var result = await response.ToModelAsync<ResultGrid<Product>>();

                Common_Rules_For_ResultGrid_Class(result, requestCount);
            }
        }

        [Fact]
        public async Task Test_Get_Pagination_Passed_Page()
        {
            using (var client = new TestClientProvider())
            {
                const int pageCount = 4;
                const int requestCountForPage = 10;
                const int requestCountForLimit = requestCountForPage * pageCount;

                var responseLimit = await client.Client.GetAsync($"/api/product?_limit={requestCountForLimit}");
                Common_Rules_For_HttpMessageMessage(responseLimit);

                var resultLimit = await responseLimit.ToModelAsync<ResultGrid<Product>>();

                Common_Rules_For_ResultGrid_Class(resultLimit, requestCountForLimit);

                var responsePage = await client.Client.GetAsync($"/api/product?_limit={requestCountForPage}&_page={pageCount}");
                Common_Rules_For_HttpMessageMessage(responsePage);

                var resultPage = await responsePage.ToModelAsync<ResultGrid<Product>>();

                Common_Rules_For_ResultGrid_Class(resultPage, requestCountForPage);

                for (var i = 0; i < requestCountForPage; i++)
                    resultPage.Data.ElementAt(i).Id.Should().Be(resultLimit.Data.ElementAt(i + (requestCountForLimit - requestCountForPage)).Id);
            }
        }

        [Fact]
        public async Task Test_Get_Pagination_With_Sorted()
        {
            using (var client = new TestClientProvider())
            {
                const int requestCount = 50;
                const string sortField = "Price";

                var response = await client.Client.GetAsync($"/api/product?_limit={requestCount}&_sort={sortField}");
                Common_Rules_For_HttpMessageMessage(response);

                var result = await response.ToModelAsync<ResultGrid<Product>>();

                Common_Rules_For_ResultGrid_Class(result, requestCount);

                for (var i = 1; i < result.Data.Count; i++)
                    result.Data.ElementAt(i).Price.Should().BeGreaterOrEqualTo(result.Data.ElementAt(i - 1).Price);
            }
        }

        [Fact]
        public async Task Test_Get_Pagination_With_Sorted_With_Asc()
        {
            using (var client = new TestClientProvider())
            {
                const int requestCount = 50;
                const string sortField = "Price";
                const string sortType = "Asc";

                var response = await client.Client.GetAsync($"/api/product?_limit={requestCount}&_sort={sortField}&_order={sortType}");
                Common_Rules_For_HttpMessageMessage(response);

                var result = await response.ToModelAsync<ResultGrid<Product>>();

                Common_Rules_For_ResultGrid_Class(result, requestCount);

                for (var i = 1; i < result.Data.Count; i++)
                    result.Data.ElementAt(i).Price.Should().BeGreaterOrEqualTo(result.Data.ElementAt(i - 1).Price);
            }
        }

        [Fact]
        public async Task Test_Get_Pagination_With_Sorted_With_Desc()
        {
            using (var client = new TestClientProvider())
            {
                const int requestCount = 50;
                const string sortField = "Price";
                const string sortType = "Desc";

                var response = await client.Client.GetAsync($"/api/product?_limit={requestCount}&_sort={sortField}&_order={sortType}");
                Common_Rules_For_HttpMessageMessage(response);

                var result = await response.ToModelAsync<ResultGrid<Product>>();

                Common_Rules_For_ResultGrid_Class(result, requestCount);

                for (var i = 1; i < result.Data.Count; i++)
                    result.Data.ElementAt(i).Price.Should().BeLessOrEqualTo(result.Data.ElementAt(i - 1).Price);
            }
        }

        [Fact]
        public async Task Test_Get_Pagination_With_Filtered()
        {
            using (var client = new TestClientProvider())
            {
                var filterPropAndNames = new Dictionary<string, string>
                {
                    {"Color", "olive"},
                    {"Currency", "USD"},
                    {"Category", "Books"}
                };

                const int requestCount = 50;

                foreach (var filterPropAndName in filterPropAndNames)
                {
                    var response = await client.Client.GetAsync(
                        $"/api/product?{filterPropAndName.Key}_like={HttpUtility.UrlEncode(filterPropAndName.Value)}&_limit={requestCount}");
                    Common_Rules_For_HttpMessageMessage(response);

                    var result = await response.ToModelAsync<ResultGrid<Product>>();

                    Common_Rules_For_ResultGrid_Class(result, requestCount);

                    foreach (var product in result.Data)
                    {
                        if (filterPropAndName.Key == "Color")
                            product.Color.Should().Be(filterPropAndName.Value);
                        else if (filterPropAndName.Key == "Currency")
                            product.Currency.Should().Be(filterPropAndName.Value);
                        else if (filterPropAndName.Key == "Category")
                            product.Category.Should().Be(filterPropAndName.Value);
                    }
                }
            }
        }

        [Fact]
        public async Task Test_Get_With_Id()
        {
            using (var client = new TestClientProvider())
            {
                var response = await client.Client.GetAsync("/api/product/1");
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResultModel<Product>>(content);

                Common_Rules_For_ResultModel_Class(result);
                Common_Rules_For_Product_Entity(result.Model);
            }
        }

        [Fact]
        public async Task Test_Put()
        {
            using (var client = new TestClientProvider())
            {
                var seller = "Murat ÖNER";
                var currency = "TL";
                var color = "red";
                var category = "Technology";
                var company = "MHG";
                var description = "Warehouse Management Software";
                var ean13 = "1112223334445";
                var mail = "info@muratoner.net";
                var name = "Warehouse Management";
                var price = 45;
                var review = "Better warehouse management software...";

                var response = await client.Client.PutAsJsonAsync($"/api/product/1", new Product
                {
                    Seller = seller,
                    Category = category,
                    Color = color,
                    Company = company,
                    Currency = currency,
                    Id = 1,
                    Description = description,
                    Ean13 = ean13,
                    Mail = mail,
                    Name = name,
                    Price = price,
                    Review = review
                });

                var result = await response.ToModelAsync<ResultModel<Product>>();

                Common_Rules_For_HttpMessageMessage(response);

                void Check(Product product)
                {
                    product.Seller.Should().Be(seller);
                    product.Currency.Should().Be(currency);
                    product.Color.Should().Be(color);
                    product.Category.Should().Be(category);
                    product.Company.Should().Be(company);
                    product.Description.Should().Be(description);
                    product.Ean13.Should().Be(ean13);
                    product.Mail.Should().Be(mail);
                    product.Name.Should().Be(name);
                    product.Price.Should().Be(price);
                    product.Review.Should().Be(review);
                }

                Check(result.Model);

                var response2 = await client.Client.GetAsync($"/api/product/{result.Model.Id}");
                var result2 = await response2.ToModelAsync<ResultModel<Product>>();
                result2.Model.Id.Should().Be(result.Model.Id);
                Check(result2.Model);
            }
        }

        [Fact]
        public async Task Test_Delete()
        {
            using (var client = new TestClientProvider())
            {
                var response = await client.Client.DeleteAsync("/api/product/1");
                var result = await response.ToModelAsync<Result>();
                Common_Rules_For_HttpMessageMessage(response);
                Common_Rules_For_ResultBase_Class(result);

                var response2 = await client.Client.GetAsync("/api/product/1");
                var result2 = await response2.ToModelAsync<ResultModel<Product>>();
                result2.Model.Should().BeNull();
            }
        }

        [Fact]
        public async Task Test_Post()
        {
            using (var client = new TestClientProvider())
            {
                var seller = "Murat ÖNER";
                var currency = "TL";
                var color = "red";
                var category = "Technology";
                var company = "MHG";
                var description = "Warehouse Management Software";
                var ean13 = "1112223334445";
                var mail = "info@muratoner.net";
                var name = "Warehouse Management";
                var price = 45;
                var review = "Better warehouse management software...";

                var response = await client.Client.PostAsJsonAsync("/api/product", new Product
                {
                    Seller = seller,
                    Currency = currency,
                    Color = color,
                    Category = category,
                    Company = company,
                    Description = description,
                    Ean13 = ean13,
                    Mail = mail,
                    Name = name,
                    Price = price,
                    Review = review
                });

                Common_Rules_For_HttpMessageMessage(response);
                var result = await response.ToModelAsync<ResultModel<Product>>();
                Common_Rules_For_ResultModel_Class(result);
                result.Model.Id.Should().BeGreaterThan(0);

                void Check(Product product)
                {
                    product.Seller.Should().Be(seller);
                    product.Currency.Should().Be(currency);
                    product.Color.Should().Be(color);
                    product.Category.Should().Be(category);
                    product.Company.Should().Be(company);
                    product.Description.Should().Be(description);
                    product.Ean13.Should().Be(ean13);
                    product.Mail.Should().Be(mail);
                    product.Name.Should().Be(name);
                    product.Price.Should().Be(price);
                    product.Review.Should().Be(review);
                }

                Check(result.Model);

                var response2 = await client.Client.GetAsync($"/api/product/{result.Model.Id}");
                var result2 = await response2.ToModelAsync<ResultModel<Product>>();
                result2.Model.Id.Should().Be(result.Model.Id);
                Check(result2.Model);
            }
        }

        private static void Common_Rules_For_Product_Entity(Product product)
        {
            product.Price.Should().BeGreaterOrEqualTo(0);

            product.Mail.Should()
                .NotBeNullOrWhiteSpace()
                .And
                .MatchRegex(TestConsts.RegexEmailPattern);

            product.Color.Should().NotBeNullOrEmpty();
            product.Company.Should().NotBeNullOrWhiteSpace();
            product.Seller.Should().NotBeNullOrWhiteSpace();
            product.Currency.Should().NotBeNullOrWhiteSpace();
            product.Category.Should().NotBeNullOrWhiteSpace();
            product.Ean13.Should().NotBeNullOrWhiteSpace();
        }

        private static void Common_Rules_For_HttpMessageMessage(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static void Common_Rules_For_ResultBase_Class(ResultBase result)
        {
            result.Errors.Count.Should().Be(0);
            result.Success.Should().Be(true);
            result.HasError.Should().Be(false);
        }

        private static void Common_Rules_For_ResultModel_Class(ResultModel<Product> result)
        {
            result.Errors.Count.Should().Be(0);
            result.Model.Should().NotBe(null);
            result.Success.Should().Be(true);
            result.HasError.Should().Be(false);
        }

        private void Common_Rules_For_ResultGrid_Class(ResultGrid<Product> result, int requestCount)
        {
            Common_Rules_For_ResultBase_Class(result);

            result.Data.Should().NotBeNull();

            result.Data.Count.Should().Be(requestCount);

            // Running common test rules for returned all product entity.
            result.Data.ForEach(Common_Rules_For_Product_Entity);
        }
    }
}
