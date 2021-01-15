using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSchool.Models;

namespace TheSchoolTests.Binders
{
    [TestClass]
    public class QnAModelBinderTest
    {
        [TestMethod]
        public void SendingValidRequest()
        {
            var form = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>() {
                {  "txtQuestion", "Question9" } ,
                { "txtTags", "Tag9"},
                { "txtAnswer", "Answer9"}
            });

            var modelState = new ModelStateDictionary();

            var response = TheSchool.Binders.QnAModelBinder.BindQnAModel(form, modelState);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(QuestionAndAnswerModel));
            Assert.IsTrue(modelState.Count == 0);
        }

        [TestMethod]
        public void SendingMissingValueRequest()
        {
            var form = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>() {
                { "txtTags", "Tag9"},
                { "txtAnswer", "Answer9"}
            });

            var modelState = new ModelStateDictionary();

            var response = TheSchool.Binders.QnAModelBinder.BindQnAModel(form, modelState);

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(QuestionAndAnswerModel));
            Assert.IsTrue(modelState.Count == 1);
            Assert.IsTrue(modelState["Question"].Errors.Count > 0);
        }
    }

}
