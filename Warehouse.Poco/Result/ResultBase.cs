using System;
using System.Collections.Generic;
using System.Linq;

namespace Warehouse.Poco.Result
{
    public abstract class ResultBase
    {
        public bool Success { get; set; }

        public ICollection<string> Errors { get; set;}

        public bool HasError => Errors.Any();

        protected ResultBase()
        {
            Errors = new List<string>();
        }

        public void SetException(Exception exception)
        {
            Success = false;
            Errors.Add(exception.Message);
        }
    }
}
