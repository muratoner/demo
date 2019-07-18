using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Warehouse.Api.Controllers;
using Warehouse.Core.Extensions;
using Warehouse.Data.Entities;
using Warehouse.Poco.Result;
using Xunit;

namespace Warehouse.Test.Api
{
    public class BaseApiTest
    {
        private ControllerBase _contoller = new ControllerBase();
        private int num1 = 10;
        private int num2 = 20;

        [Fact]
        public void Test_ToResult_One_Param()
        {
            var res = _contoller.ToResult(CheckWrongNum, num1);
            res.Errors.ElementAt(0).Should().StartWith("Expected");
            Common_Rules_For_ResultBase_Class_UnSuccess(res);

            res = _contoller.ToResult(CheckCorrectNum, num1);
            Common_Rules_For_ResultBase_Class_Success(res);
        }

        [Fact]
        public void Test_ToResult_One_Param_With_Return_Type()
        {
            var res = _contoller.ToResult(CheckWrongValWithReturnType, num1);
            res.Errors.ElementAt(0).Should().StartWith("Expected");
            Common_Rules_For_ResultBase_Class_UnSuccess(res);

            res = _contoller.ToResult(CheckCorrectValWithReturnType, num1);
            Common_Rules_For_ResultBase_Class_Success(res);
        }

        [Fact]
        public void Test_ToResultModel_One_Param()
        {
            var res = _contoller.ToResultModel(CheckWrongValWithReturnType, num1);
            res.Errors.ElementAt(0).Should().StartWith("Expected");
            Common_Rules_For_ResultModel_Class_UnSuccess(res);

            res = _contoller.ToResultModel(CheckCorrectValWithReturnType, num1);
            Common_Rules_For_ResultModel_Class_Success(res);
        }

        [Fact]
        public void Test_ToResultModel_Two_Params()
        {
            var res = _contoller.ToResultModel(CheckWrongValWithReturnType, num1, num2);
            res.Errors.ElementAt(0).Should().StartWith("Expected");
            Common_Rules_For_ResultModel_Class_UnSuccess(res);

            res = _contoller.ToResultModel(CheckCorrectValWithReturnType, num1, num2);
            Common_Rules_For_ResultModel_Class_Success(res);
        }

        [Fact]
        public void Test_ToResultGrid_No_Param()
        {
            var res = _contoller.ToResultData((param1, param2, param3, param4) =>
            {
                param1.Should().Be(param2);
                return new KeyValuePair<ICollection<Product>, int>();
            }, num1, num2, 0 , 1);
            res.Errors.ElementAt(0).Should().StartWith("Expected");
            Common_Rules_For_ResultGrid_Class_UnSuccess(res, 5);

            res = _contoller.ToResultData((param1, param2, param3, param4) => new KeyValuePair<ICollection<Product>, int>( new List<Product>(), 0 ), num1, num2, 0, 0);
            Common_Rules_For_ResultGrid_Class_Success(res, 0);
        }

        private void CheckWrongNum<TParam>(TParam param1)
        {
            param1.Should().Be(1);
        }

        private void CheckCorrectNum<TParam>(TParam param1)
        {
            param1.Should().Be(num1);
        }

        private bool CheckCorrectValWithReturnType<TParam>(TParam param1)
        {
            param1.Should().Be(num1);
            return true;
        }

        private bool CheckWrongValWithReturnType<TParam>(TParam param1)
        {
            param1.Should().Be(1);
            return true;
        }

        private bool CheckCorrectValWithReturnType<TParam1, TParam2>(TParam1 param1, TParam2 param2)
        {
            param1.Should().Be(num1);
            param2.Should().Be(num2);
            return true;
        }

        private bool CheckWrongValWithReturnType<TParam1, TParam2>(TParam1 param1, TParam2 param2)
        {
            param1.Should().Be(1);
            param2.Should().Be(2);
            return true;
        }

        #region Common Rules
        private static void Common_Rules_For_ResultBase_Class_Success(ResultBase result)
        {
            result.Errors.Count.Should().Be(0);
            result.Success.Should().Be(true);
            result.HasError.Should().Be(false);
        }

        private static void Common_Rules_For_ResultBase_Class_UnSuccess(ResultBase result)
        {
            result.Errors.Count.Should().BeGreaterThan(0);
            result.Success.Should().Be(false);
            result.HasError.Should().Be(true);
        }

        private static void Common_Rules_For_ResultModel_Class_Success<TEntity>(ResultModel<TEntity> result)
        {
            result.Errors.Count.Should().Be(0);
            result.Model.Should().Be(true);
            result.Success.Should().Be(true);
            result.HasError.Should().Be(false);
        }

        private static void Common_Rules_For_ResultModel_Class_UnSuccess<TEntity>(ResultModel<TEntity> result)
        {
            result.Errors.Count.Should().BeGreaterThan(0);
            result.Model.Should().Be(false);
            result.Success.Should().Be(false);
            result.HasError.Should().Be(true);
        }

        private void Common_Rules_For_ResultGrid_Class_Success<TEntity>(ResultGrid<TEntity> result, int requestCount)
        {
            Common_Rules_For_ResultBase_Class_Success(result);

            result.Data.Should().NotBeNull();
            result.Data.Count.Should().Be(requestCount);
        }

        private void Common_Rules_For_ResultGrid_Class_UnSuccess<TEntity>(ResultGrid<TEntity> result, int requestCount)
        {
            Common_Rules_For_ResultBase_Class_UnSuccess(result);

            result.Data.Should().BeNull();
        }
        #endregion
    }
}
