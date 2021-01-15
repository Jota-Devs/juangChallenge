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
    public class SingletonTodosHelper : ApiHelper<Todos>
    {
        private static volatile IQueryable<Todos> _localCache;
        private static object _lock = new object();

        public SingletonTodosHelper(IRestClient restClient, ISerializer serializer) 
            : base(restClient, serializer, "todos")
        {
        }

        public new IQueryable<Todos> GetAll()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////
            //TODO: Make your code changes here so we can keep the same reference value object all the time.
            //base.GetAll() should be called once irrespectively of how many instances of SingletonPhotoHelper 
            //are created. You can change LocalCache definition.
           
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
