

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TheSchool.Data;
using TheSchool.Entities;

namespace TheSchoolTests.Data.Test
{
    [TestClass]
    public class KnowledgeBaseDataTests
    {
        [TestInitialize]
        public void Init()
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            using (WisperoDbContext db = new WisperoDbContext())
            {
                db.KnowledgeBaseItems.Add(new KnowledgeBaseItem() { Id = 1, Answer = "Answer1", Query = "Question1", Tags = "Tag1, Tag2", LastUpdateOn = DateTime.Now });
                db.KnowledgeBaseItems.Add(new KnowledgeBaseItem() { Id = 2, Answer = "Answer2", Query = "Question2", Tags = "Tag3, Tag2", LastUpdateOn = DateTime.Now });

                db.SaveChanges();
            }
        }

        [TestCleanup]
        public void Clean()
        {
            using (WisperoDbContext db = new WisperoDbContext())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[KnowledgeBaseItems]");
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void Adding()
        {
            using (var db = new WisperoDbContext())
            {
                var dataService = new KnowledgeBaseData(db);

                dataService.Add(new KnowledgeBaseItem() { Answer = "Answer3", Query = "Question3", Tags = "Tag4, Tag1", LastUpdateOn = DateTime.Now });
                dataService.CommitChanges();

                var recentlyAdded = dataService.GetByFilter(x => x.Answer == "Answer3");

                Assert.IsNotNull(recentlyAdded);
                Assert.IsTrue(recentlyAdded.Count > 0);
                Assert.IsTrue(recentlyAdded[0].Query == "Question3");
            }
        }

        [TestMethod]
        public void Deleting()
        {
            using (var db = new WisperoDbContext())
            {
                var dataService = new KnowledgeBaseData(db);

                dataService.Delete(1);
                dataService.CommitChanges();

                var recentlyAdded = dataService.Get(1);

                Assert.IsNull(recentlyAdded);
            }
        }

        [TestMethod]
        public void Editing()
        {
            using (var db = new WisperoDbContext())
            {
                var dataService = new KnowledgeBaseData(db);

                var currentEntity = dataService.Get(2);

                //Making updates
                currentEntity.Answer = "NewAnswer2";
                currentEntity.Query = "NewQuery2";
                currentEntity.LastUpdateOn = DateTime.Now;
                currentEntity.Tags = "NewTags";

                dataService.Edit(currentEntity);
                dataService.CommitChanges();

                var recentlyUpdated = dataService.Get(2);

                Assert.IsNotNull(recentlyUpdated);
                Assert.IsTrue(recentlyUpdated.Query == "NewQuery2");
            }
        }

        private KnowledgeBaseItem GetItemUpdated(int id, string postFix)
        {
            using (var db = new WisperoDbContext())
            {
                var dataService = new KnowledgeBaseData(db);
                var currentEntity = dataService.Get(id);

                //Making updates
                currentEntity.Answer = "NewAnswer" + postFix;
                currentEntity.Query = "NewQuery" + postFix;
                currentEntity.LastUpdateOn = DateTime.Now;

                return currentEntity;
            }
        }

        [TestMethod]
        public void EditingConcurrent()
        {
            var entityA = GetItemUpdated(2, "A");
            var entityB = GetItemUpdated(2, "B");

            using (var db = new WisperoDbContext())
            {
                var dataService = new KnowledgeBaseData(db);
                Assert.IsTrue(entityA.RowVersion.SequenceEqual(entityB.RowVersion));

                //Updating EntityA.
                dataService.Edit(entityA);
                dataService.CommitChanges();

                var recentlyUpdated = dataService.Get(2);
                Assert.IsNotNull(recentlyUpdated.Query);
                Assert.IsTrue(recentlyUpdated.Query == entityA.Query);

                //Sleep to simulate a delay between updates.
                System.Threading.Thread.Sleep(10);

                //Updating EntityB. 
                //It should failed because the version kept memory has changes on the source because of EntityA.
                try
                {
                    dataService.Edit(entityB);
                    dataService.CommitChanges();
                    Assert.Fail();
                }
                catch (DbUpdateConcurrencyException)
                {
                }
            }
        }

        [TestMethod]
        public void Getting()
        {
            using (var db = new WisperoDbContext())
            {
                var dataService = new KnowledgeBaseData(db);
                var retValue = dataService.Get(2);
                Assert.IsNotNull(retValue);
                Assert.IsTrue(retValue.Id == 2);
            }
        }

        [TestMethod]
        public void GettingAll()
        {
            using (var db = new WisperoDbContext())
            {
                var dataService = new KnowledgeBaseData(db);
                var retValue = dataService.GetAll();
                Assert.IsNotNull(retValue);
                Assert.IsTrue(retValue.Count > 0);
            }
        }

        [TestMethod]
        public void GettingByFilter()
        {
            using (var db = new WisperoDbContext())
            {
                var dataService = new KnowledgeBaseData(db);
                var retValue = dataService.GetByFilter(x=>x.Tags.Contains("Tag3"));

                Assert.IsNotNull(retValue);
                Assert.IsTrue(retValue.Count > 0);
                Assert.IsTrue(retValue[0].Id == 2);
            }
        }
    }
}
