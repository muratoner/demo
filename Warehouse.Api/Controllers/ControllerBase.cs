using System;
using System.Collections.Generic;
using Warehouse.Poco.Result;

namespace Warehouse.Api.Controllers
{
    public class ControllerBase
    {
        #region ToResult
        public Result ToResult<TParam1>(Action<TParam1> act, TParam1 param1)
            => _ToResult(() => act(param1));
        public Result ToResult<TEntity, TParam1>(Func<TParam1, TEntity> act, TParam1 param1)
            => _ToResult(() => act(param1));
        #endregion

        #region ToResultModel
        public ResultModel<TModel> ToResultModel<TModel,TParam1>(Func<TParam1, TModel> act, TParam1 param1)
            => _ToResultModel(act, param1);
        #endregion

        #region ToResultData
        public ResultGrid<TEntity> ToResultData<TEntity>(Func<KeyValuePair<ICollection<TEntity>, int>> func)
            => _ToResultData(func);

        public ResultGrid<TEntity> ToResultData<TEntity, TParam1>(
            Func<TParam1, KeyValuePair<ICollection<TEntity>, int>> func, 
            TParam1 param1)
            => _ToResultData(() => func(param1));

        public ResultGrid<TEntity> ToResultData<TEntity, TParam1, TParam2>(
            Func<TParam1, TParam2, KeyValuePair<ICollection<TEntity>, int>> func, 
            TParam1 param1, 
            TParam2 param2)
            => _ToResultData(() => func(param1, param2));

        public ResultGrid<TEntity> ToResultData<TEntity, TParam1, TParam2, TParam3>(
            Func<TParam1, TParam2, TParam3, KeyValuePair<ICollection<TEntity>, int>> func,
            TParam1 param1,
            TParam2 param2,
            TParam3 param3)
            => _ToResultData(() => func(param1, param2, param3));

        public ResultGrid<TEntity> ToResultData<TEntity, TParam1, TParam2, TParam3, TParam4>(
            Func<TParam1, TParam2, TParam3, TParam4, KeyValuePair<ICollection<TEntity>, int>> func, 
            TParam1 param1, 
            TParam2 param2, 
            TParam3 param3, 
            TParam4 param4)
            => _ToResultData(() => func(param1, param2, param3, param4));

        public ResultGrid<TEntity> ToResultData<TEntity, TParam1, TParam2, TParam3, TParam4, TParam5>(
            Func<TParam1, TParam2, TParam3, TParam4, TParam5, KeyValuePair<ICollection<TEntity>, int>> func,
            TParam1 param1,
            TParam2 param2,
            TParam3 param3,
            TParam4 param4,
            TParam5 param5)
            => _ToResultData(() => func(param1, param2, param3, param4, param5));

        public ResultGrid<TEntity> ToResultData<TEntity, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(
            Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, KeyValuePair<ICollection<TEntity>, int>> func,
            TParam1 param1,
            TParam2 param2,
            TParam3 param3,
            TParam4 param4,
            TParam5 param5,
            TParam6 param6)
            => _ToResultData(() => func(param1, param2, param3, param4, param5, param6));
        #endregion

        #region Private

        private static ResultModel<TModel> _ToResultModel<TModel, TParam1>(Func<TParam1, TModel> func, TParam1 param1)
        {
            var res = new ResultModel<TModel>
            {
                Success = true
            };
            try
            {
                res.Model = func(param1);
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Errors.Add(ex.Message);
            }
            return res;
        }

        private static Result _ToResult(Action act)
        {
            var res = new Result
            {
                Success = true
            };
            try
            {
                act();
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Errors.Add(ex.Message);
            }
            return res;
        }

        private static ResultGrid<TEntity> _ToResultData<TEntity>(Func<KeyValuePair<ICollection<TEntity>, int>> func)
        {
            var res = new ResultGrid<TEntity>
            {
                Success = true
            };
            try
            {
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
