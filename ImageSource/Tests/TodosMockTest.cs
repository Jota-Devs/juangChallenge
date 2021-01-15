using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageSource.Helper;
using Moq;
using ImageSource.Concrete;

namespace ImageSource
{
    /// <summary>
    /// THIS TEST SUITE HAS BEEN CREATED TO VALIDATE THE BEHAVIOR OF THE PROGRAM 
    /// WITHOUT ACTUALLY CALLING THE REST-API SERVICE.
    /// MOCKING DATA AND CALLS ARE REQUIRED.
    /// </summary>
    [TestClass]
    public class TodosMockTest
    {
        ISerializer serializer;
        Mock<IRestClient> client;
        ApiHelper<Photo> helper;

        [TestInitialize]
        public void TestInitializer()
        {
            serializer = new SerializerImp();
            var rawJson = "[{'albumId':1,'id':9,'title':'accusamus beatae ad facilis vol similique qui dolor','url':'http://placehold.it/600/92c952','thumbnailUrl':'http://placehold.it/150/92c952'},{'albumId':1,'id':2,'title':'reprehenderit est deserunt velit dolor','url':'http://placehold.it/600/771796','thumbnailUrl':'http://placehold.it/150/771796'},{'albumId':1,'id':3,'title':'officia porro iure quia iusto qui ipsa ut modi','url':'http://placehold.it/600/24f355/vol','thumbnailUrl':'http://placehold.it/150/24f355'},{'albumId':1,'id':4,'title':'culpa odio esse rerum omnis laboriosam voluptate repudiandae','url':'http://placehold.it/600/d32776','thumbnailUrl':'http://placehold.it/150/d32776'},{'albumId':1,'id':5,'title':'natus nisi omnis corporis facere molestiae rerum in','url':'http://placehold.it/600/f66b97','thumbnailUrl':'http://placehold.it/150/f66b97'},{'albumId':1,'id':6,'title':'accusamus ea aliquid et amet sequi nemo','url':'http://placehold.it/600/56a8c2','thumbnailUrl':'http://placehold.it/150/56a8c2'}]";

            /////////////////////////////////////////////////////////////////////////////////
            //TODO: Use Moq.Mocks as we want to use rawJason as data source, your code here..
           

            throw new NotImplementedException();
            /////////////////////////////////////////////////////////////////////////////////
        }

        /// <summary>
        /// DO NOT CHANGE ANYTHING HERE.
        /// </summary>
        [TestMethod]
        public void GetAll_AllFieldsWithValuesTest()
        {
            var output = helper.GetAll();
            helper.Match(x => x.Title.Contains("demo") || x.Url.Contains("demo"));

            Assert.IsNotNull(output);
            Assert.IsTrue(output.Count() > 0);
            Assert.IsTrue(output.First().PhotoBook > 0);
        }

        /// <summary>
        /// DO NOT CHANGE ANYTHING HERE.
        /// </summary>
        [TestMethod]
        public void GetAll_FindMatches()
        {
            var output = helper.Match(x => x.Title.Contains("vol") || x.Url.Contains("vol"));

            Assert.IsNotNull(output);
            Assert.IsTrue(output.Count() > 0 && output.Count() < 5000);
            Assert.IsTrue(output.First().Title.Contains("vol") || output.First().Url.Contains("vol"));
        }

        /// <summary>
        /// DO NOT CHANGE ANYTHING HERE.
        /// </summary>
        [TestMethod]
        public void GetAll_FindMatchesAndSort()
        {
            var output1 = helper.Match(
                searchPattern: x => x.Title.Contains("dolor") || x.Url.Contains("dolor"),
                sorting: x => x.Title);

            var output2 = helper.Match(
                searchPattern: x => x.Title.Contains("dolor") || x.Url.Contains("dolor"),
                sorting: x => x.Id);

            Assert.IsNotNull(output1);
            Assert.IsNotNull(output2);
            Assert.IsTrue(output1.First().Title != output2.First().Title);
            Assert.IsTrue(client.Object.HitCounter > 1);
        }
    }
}
