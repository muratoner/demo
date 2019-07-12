using System.Collections.Generic;

namespace Warehouse.Poco.Result
{
    public class ResultGrid<TEntity> : ResultBase
    {
        public ICollection<TEntity> Data { get; set; }

        public int Count { get; set; }
    }
}
