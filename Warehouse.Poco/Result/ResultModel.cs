namespace Warehouse.Poco.Result
{
    public class ResultModel<TModel> : Result
    {
        public TModel Model { get; set; }
    }
}
