using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageSource.Helper
{
    public class CacheTodosHelper : ApiHelper<Todos>
    {
        private static volatile MemoryCache _localCache;
        private static object _lock = new object();

        public CacheTodosHelper(IRestClient restClient, ISerializer serializer, MemoryCache localCache) 
            : base(restClient, serializer, "todos")
        {
            _localCache = localCache;
        }

        public new IQueryable<Todos> GetAll()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////
            //TODO: Make your code changes here so we can keep the same reference value object all the time.
            //base.GetAll() should be called once irrespectively of how many instances of CacheTodosHelper 
            //are created. Implement MemoryCache to keep the results from base.GetAll() cached for 20 seconds.
            

            throw new NotImplementedException();
            /////////////////////////////////////////////////////////////////////////////////////////////
        }
        
        public new IQueryable<Todos> Match(Expression<Func<Todos, bool>> searchPattern)
        {
            var collection = this.GetAll();
            return collection.Where(searchPattern);
        }   

        public new IQueryable<Todos> Match(Expression<Func<Todos, bool>> searchPattern, Expression<Func<Todos, object>> sorting)
        {
            return Match(searchPattern).OrderBy(sorting);
        }
    }
}
