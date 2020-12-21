using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;

namespace MicroAppPOC.Infrastructure.Database
{
    public class SqliteGtfsGenericRepository<T> : IGtfsGenericRepository<T> where T : class, new()
    {
        private readonly SQLiteAsyncConnection _db;

        public SqliteGtfsGenericRepository(SQLiteAsyncConnection db)
        {
            _db = db;
            _db.CreateTableAsync<T>();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await _db.Table<T>().ToListAsync();
            
            var result = _db.Table<T>().Where(predicate);
            
            return await result.ToListAsync();
        }

        public async Task Bulk(IEnumerable<T> entities)
        {
            await _db.InsertAllAsync(entities);
        }
    }
}