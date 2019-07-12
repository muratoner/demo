using System;
using System.Collections.Generic;

namespace Warehouse.Core.Extensions
{
    public static class ExtensionEnumerable
    {
        public static void ForEach<TEntity>(this IEnumerable<TEntity> items, Action<TEntity> action)
        {
            foreach (var item in items)
                action(item);
        }
    }
}
