using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageSource.Helper;
using ImageSource.Concrete;

namespace ImageSource
{
    [TestClass]
    [TestCategory("Integration")]
    public class SingletonTodosIntegratedTest
    {
        ISerializer serializer;
        IRestClient client;
        SingletonTodosHelper helper;

        [TestInitialize]
        public void TestInitializer()
        {
            serializer = new SerializerImp();
            client = new RestClientImpl();

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
            var output = helper.Match(x => x.Title.Contains("vol"));
            
            Assert.IsNotNull(output);
            Assert.IsTrue(output.Count() > 0 && output.Count() < 5000);
            Assert.IsFalse(string.IsNullOrEmpty(output.First().Title));
            
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Match_FindMatchesAndSort()
        {

            var output1 = helper.Match(
                searchPattern: x => x.Title.Contains("dolor"), 
                sorting: x => x.Title);

            var output2 = helper.Match(
                searchPattern: x => x.Title.Contains("dolor"),
                sorting: x => x.Id);

            Assert.IsNotNull(output1);
            Assert.IsNotNull(output2);
            Assert.IsTrue(output1.First().Title != output2.First().Title);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void GetAll_ValidatingSingletonCache()
        {
            var helper1 = new SingletonTodosHelper(client, serializer);
            var output1 = helper1.GetAll();

            var helper2 = new SingletonTodosHelper(client, serializer);
            var output2 = helper2.GetAll();

            Assert.IsNotNull(output1);
            Assert.IsNotNull(output2);
            Assert.AreSame(output1, output2);
        }
    }
}
