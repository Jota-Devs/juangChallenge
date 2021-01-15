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
    public class ApiHelper<T>
    {
        public IRestClient RestClient;
        public ISerializer Serializer;
        private readonly string _targetApi;

        public ApiHelper(IRestClient restClient, ISerializer serializer, string targetApi)
        {
            RestClient = restClient;
            Serializer = serializer;
            _targetApi = targetApi;
        }

        public IQueryable<T> GetAll()
        {
            var rawContent = RestClient.Get($"https://jsonplaceholder.typicode.com/{_targetApi}");
            var collection = Serializer.ToObject<IList<T>>(rawContent);

            return collection.AsQueryable();
        }
        
        public IQueryable<T> Match(Expression<Func<T, bool>> searchPattern)
        {
            var collection = this.GetAll();
            return collection.Where(searchPattern);
        }

        public IQueryable<T> Match(Expression<Func<T, bool>> searchPattern, Expression<Func<T, object>> sorting)
        {
            return Match(searchPattern).OrderBy(sorting);
        }
    }
}
