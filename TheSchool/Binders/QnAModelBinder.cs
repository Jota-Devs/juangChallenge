
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TheSchool.Models;//add

namespace TheSchool.Binders
{
    public class QnAModelBinder : IModelBinder
    {
        public static object BindQnAModel(IFormCollection values, ModelStateDictionary modelState)
        {
            //TODO: Implement model binder for QuestionAndAnswerModel
            var qna = new QuestionAndAnswerModel(); 

            qna.Question = values["txtQuestion"]; 
            qna.Answer = values["txtAnswer"];
            qna.Tags = values["txtTags"];

            if (string.IsNullOrEmpty(qna.Question))
            {
                modelState.AddModelError("Question", "error question");
            }
            else if (string.IsNullOrEmpty(qna.Answer))
            {
                modelState.AddModelError("Answer", "error Answer");
            }
            else if (string.IsNullOrEmpty(qna.Tags))
            {
                modelState.AddModelError("Tags", "error Tags");
            }
            return qna; 
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            return Task.FromResult(BindQnAModel(bindingContext.HttpContext.Request.Form, bindingContext.ModelState));
        }
    }
}