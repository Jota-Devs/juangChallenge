using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageSource.Helper;
using ImageSource.Concrete;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace ImageSource
{
    [TestClass]
    [TestCategory("Integration")]
    public class CacheTodosIntegratedTest
    {
        ISerializer serializer;
        IRestClient client;
        MemoryCache options;
        SingletonTodosHelper helper;

        [TestInitialize]
        public void TestInitializer()
        {
            serializer = new SerializerImp();
            client = new RestClientImpl();
            
            options = new MemoryCache(new MemoryCacheOptions());

            helper = new SingletonTodosHelper(client, serializer);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetAll_AllFieldsWithValuesTest()
        {
            var output = helper.GetAll();

            Assert.IsNotNull(output);
            Assert.IsTrue(output.Count() > 0);
            Assert.IsFalse(string.IsNullOrEmpty(output.First().Title));
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Match_FindMatches()
        {
            var output = helper.Match(x => x.Title.Contains("vol") );
            
            Assert.IsNotNull(output);
            Assert.IsTrue(output.Count() > 0 && output.Count() < 5000);
            Assert.IsTrue(output.First().Title.Contains("vol"));
            
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Match_FindMatchesAndSort()
        {

            var output1 = helper.Match(
                searchPattern: x => x.Title.Contains("dolor") , 
                sorting: x => x.Title);

            var output2 = helper.Match(
                searchPattern: x => x.Title.Contains("dolor") ,
                sorting: x => x.Id);

            Assert.IsNotNull(output1);
            Assert.IsNotNull(output2);
            Assert.IsTrue(output1.First().Title != output2.First().Title);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetAll_ValidatingCacheValues()
        {
            var helper1 = new CacheTodosHelper(client, serializer, options);
            var output1 = helper1.GetAll();

            var helper2 = new CacheTodosHelper(client, serializer, options);
            var output2 = helper2.GetAll();

            Assert.IsNotNull(output1);
            Assert.IsNotNull(output2);
            Assert.AreSame(output1, output2);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetAll_ValidatingCacheExpiration()
        {
            var helper1 = new CacheTodosHelper(client, serializer, options);
            var output1 = helper1.GetAll();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));

            var helper2 = new CacheTodosHelper(client, serializer, options);
            var output2 = helper2.GetAll();

            Assert.IsNotNull(output1);
            Assert.IsNotNull(output2);
            Assert.AreNotSame(output1, output2);
        }
    }
}
