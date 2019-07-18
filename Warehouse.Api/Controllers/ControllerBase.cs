using System;
using System.Collections.Generic;
using FluentValidation;
using Warehouse.Core.Extensions;
using Warehouse.Poco.Result;

namespace Warehouse.Api.Controllers
{
    public class ControllerBase
    {
        #region ToResult
        public Result ToResult<TParam1>(
            Action<TParam1> act, 
            TParam1 param1, 
            IValidator validation = null, 
            object validationType = null)
                => _ToResult(() => act(param1), validation, validationType);

        public Result ToResult<TEntity, TParam1>(
            Func<TParam1, TEntity> func, 
            TParam1 param1, 
            IValidator validation = null, 
            object validationType = null)
                => _ToResult(() => func(param1), validation, validationType);
        #endregion

        #region ToResultModel
        public ResultModel<TModel> ToResultModel<TModel,TParam1>(
            Func<TParam1, TModel> func, 
            TParam1 param1, 
            IValidator validation = null, 
            object validationType = null)
                => _ToResultModel(() => func(param1), validation, validationType);

        public ResultModel<TModel> ToResultModel<TModel,TParam1,TParam2>(
            Func<TParam1, TParam2, TModel> act, 
            TParam1 param1, 
            TParam2 param2,
            IValidator validation = null, 
            object validationType = null)
                => _ToResultModel(() => act(param1, param2), validation, validationType);
        #endregion

        #region ToResultData
        public ResultGrid<TEntity> ToResultData<TEntity, TParam1, TParam2, TParam3, TParam4>(
            Func<TParam1, TParam2, TParam3, TParam4, KeyValuePair<ICollection<TEntity>, int>> func, 
            TParam1 param1, 
            TParam2 param2, 
            TParam3 param3, 
            TParam4 param4, 
            IValidator validation = null, 
            object validationType = null)
                => _ToResultData(() => func(param1, param2, param3, param4), validation, validationType);
        #endregion

        #region Private

        private static ResultModel<TModel> _ToResultModel<TModel>(
            Func<TModel> func,
            IValidator validation, 
            object validationType)
        {
            var res = new ResultModel<TModel>
            {
                Success = true
            };
            try
            {
                var result = validation?.Validate(validationType);
                result?.Errors?.ForEach((err) => res.Errors.Add(err.ErrorMessage));
                if (res.HasError) return res;

                res.Model = func();
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Errors.Add(ex.Message);
            }
            return res;
        }

        private static Result _ToResult(
            Action act, 
            IValidator validation, 
            object validationType)
        {
            var res = new Result
            {
                Success = true
            };
            try
            {
                var result = validation?.Validate(validationType);
                result?.Errors?.ForEach((err) => res.Errors.Add(err.ErrorMessage));
                if (res.HasError) return res;

                act();
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Errors.Add(ex.Message);
            }
            return res;
        }

        private static ResultGrid<TEntity> _ToResultData<TEntity>(
            Func<KeyValuePair<ICollection<TEntity>, int>> func, 
            IValidator validation, 
            object validationType)
        {
            var res = new ResultGrid<TEntity>
            {
                Success = true
            };
            try
            {
                var result = validation?.Validate(validationType);
                result?.Errors?.ForEach((err) => res.Errors.Add(err.ErrorMessage));
                if (res.HasError) return res;

                var (key, value) = func();
                res.Data = key;
                res.Count = value;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Errors.Add(ex.Message);
            }
            return res;
        }
        #endregion
    }
}
