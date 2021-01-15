
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TheSchool.Data.Helpers;
using TheSchool.Entities;
using TheSchool.Services;

namespace TheSchoolTests.Data.Test
{
    [TestClass]
    public class TagHelperTests
    {
        [TestMethod]
        public void DependencyInjected()
        {
            int max;
            Moq.Mock<IQueryService<KnowledgeBaseItem>> queryService = new Moq.Mock<IQueryService<KnowledgeBaseItem>>();

            queryService.Setup(x => x.GetAll()).Returns(
              new System.Collections.Generic.List<KnowledgeBaseItem>() {
                    new KnowledgeBaseItem { Id = 1, Query = "Question1", Answer = "Answer1", Tags = "Tag1, Tag2", LastUpdateOn = DateTime.Now },
                    new KnowledgeBaseItem { Id = 2, Query = "Question2", Answer = "Answer2", Tags = "Tag2, Tag3", LastUpdateOn = DateTime.Now }
              });

            var results = TagHelper.Process(queryService.Object, out max);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count == 3);
            Assert.IsTrue(max == 2);
        }

        [TestMethod]
        public void DirectCallWithoutDependencies()
        {
            int max;
            var collection = 
              new System.Collections.Generic.List<KnowledgeBaseItem>() {
                    new KnowledgeBaseItem { Id = 1, Query = "Question1", Answer = "Answer1", Tags = "Tag1, Tag2", LastUpdateOn = DateTime.Now },
                    new KnowledgeBaseItem { Id = 2, Query = "Question2", Answer = "Answer2", Tags = "Tag2, Tag3", LastUpdateOn = DateTime.Now },
                    new KnowledgeBaseItem { Id = 2, Query = "Question2", Answer = "Answer2", Tags = "Tag4, Tag2, Tag3", LastUpdateOn = DateTime.Now }
              };

            var results = TagHelper.Process(collection, out max);

            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count == 4);
            Assert.IsTrue(max == 3);
        }
    }
}
