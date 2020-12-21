using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MicroAppPOC.Infrastructure.Database
{
    public interface IGtfsGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate = null);
        Task Bulk(IEnumerable<T> entities);
    }
}