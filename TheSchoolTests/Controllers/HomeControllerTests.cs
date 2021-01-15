
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using TheSchool.Controllers;
using TheSchool.Entities;
using TheSchool.Models;

namespace TheSchoolTests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        Moq.Mock<TheSchool.Services.IDataService<KnowledgeBaseItem>> dataService;
        Moq.Mock<TheSchool.Services.IQueryService<KnowledgeBaseItem>> queryService;
        Moq.Mock<ILogger<HomeController>> logger;

        [TestInitialize]
        public void Initialize()
        {
            dataService = new Moq.Mock<TheSchool.Services.IDataService<KnowledgeBaseItem>>();
            queryService = new Moq.Mock<TheSchool.Services.IQueryService<KnowledgeBaseItem>>();
            queryService.Setup(x => x.GetAll()).Returns(new List<KnowledgeBaseItem>() { 
                new KnowledgeBaseItem() { Id = 1, Answer = "sample answer", Query="sample answer", Tags="sample tag" }
            });


            logger = new Moq.Mock<ILogger<HomeController>>();
        }

        [TestMethod]
        public void Index_LoadingPage()
        {
            var controller = new HomeController(logger.Object, dataService.Object, queryService.Object);
            var viewResult = controller.Index();

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(ViewResult));
            Assert.IsNotNull((viewResult as ViewResult).Model);
            Assert.AreEqual(((viewResult as ViewResult).Model as HomeViewModel).Tags.MaxCount, 1);
        }

        [TestMethod]
        public void Entry_LoadingPartialView()
        {
            var controller = new HomeController(logger.Object, dataService.Object, queryService.Object);
            var viewResult = controller.Entry();

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(PartialViewResult));
        }

        [TestMethod]
        public void TagCloud_LoadingPartialView()
        {
            queryService.Setup(x => x.GetAll()).Returns(
               new System.Collections.Generic.List<KnowledgeBaseItem>() {
                    new KnowledgeBaseItem { Id = 1, Query = "Question1", Answer = "Answer1", Tags = "Tag1, Tag2", LastUpdateOn = DateTime.Now },
                    new KnowledgeBaseItem { Id = 2, Query = "Question2", Answer = "Answer2", Tags = "Tag2, Tag3", LastUpdateOn = DateTime.Now }
               });

            var controller = new HomeController(logger.Object, dataService.Object, queryService.Object);
            var viewResult = controller.TagCloud();

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(PartialViewResult));

            var model = ((PartialViewResult)viewResult).Model as TagCloudModel;

            Assert.IsTrue(model.MaxCount == 2);
            Assert.IsTrue(model.Tags.Count == 3);
        }

        [TestMethod]
        public void TagCloud_EmptyList()
        {
            queryService.Setup(x => x.GetAll()).Returns(new System.Collections.Generic.List<KnowledgeBaseItem>() { });

            var controller = new HomeController(logger.Object, dataService.Object, queryService.Object);
            var viewResult = controller.TagCloud();

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(PartialViewResult));

            var model = ((PartialViewResult)viewResult).Model as TagCloudModel;

            Assert.IsTrue(model.MaxCount == 0);
            Assert.IsTrue(model.Tags.Count == 0);
        }

        [TestMethod]
        public void New_ValidQuestion()
        {
            var inputModel = new QuestionAndAnswerModel() { Question = "Question1", Answer = "Answer1", Tags = "Tag1, Tag2" };

            dataService.Setup(x => x.Add(It.IsAny<KnowledgeBaseItem>()));

            var controller = new HomeController(logger.Object, dataService.Object, queryService.Object);
            var viewResult = controller.New(inputModel);

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(RedirectToActionResult));
            Assert.AreEqual((viewResult as RedirectToActionResult).ActionName, "Index");
        }

        [TestMethod]
        public void New_QuestionIncomplete()
        {
            var inputModel = new QuestionAndAnswerModel() { Question = "Question1", Answer = null, Tags = "Tag1, Tag2" };
            
            dataService.Setup(x => x.Add(It.IsAny<KnowledgeBaseItem>()));
            
            var controller = new HomeController(logger.Object, dataService.Object, queryService.Object);
            controller.ModelState.AddModelError("Answer", "Answer is required");

            var viewResult = controller.New(inputModel);

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(RedirectToActionResult));
            Assert.AreEqual((viewResult as RedirectToActionResult).ActionName, "Index");
        }
    }
}
