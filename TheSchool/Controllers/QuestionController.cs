
using Microsoft.AspNetCore.Mvc;
using System;
using TheSchool.Models;

namespace TheSchool.Controllers
{
    public class QuestionController : Controller
    {
        Services.IDataService<Entities.KnowledgeBaseItem> KnowledgeData;
        Services.IQueryService<Entities.KnowledgeBaseItem> KnowledgeQuery;
        readonly AutoMapper.IMapper mapper;

        public QuestionController(Services.IDataService<Entities.KnowledgeBaseItem> knowledgeData, Services.IQueryService<Entities.KnowledgeBaseItem> knowledgeQuery)
        {
            KnowledgeData = knowledgeData;
            KnowledgeQuery = knowledgeQuery;

            //TODO: Implement mapping as needed.

            var configurationManager = new AutoMapper.MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Entities.KnowledgeBaseItem, QuestionAndAnswerEditModel>()
                        .ForMember(kb => kb.Question, opt => opt.MapFrom(qna => qna.Query)); //mapper edit get

                    cfg.CreateMap<QuestionAndAnswerEditModel, Entities.KnowledgeBaseItem>()
                        .ForMember(kb => kb.Query, opt => opt.MapFrom(qna => qna.Question)); //mapper edit post
                }); 

            mapper = configurationManager.CreateMapper();
        }
        // GET: Question
        public ActionResult Edit(int id)
        {
            //TODO: Implement this method to retrieve and present data for Edition/Updates.
            var retQnA = mapper.Map<Entities.KnowledgeBaseItem, QuestionAndAnswerEditModel>(KnowledgeQuery.Get(id));
            if (retQnA != null)
            {
                return View(retQnA);
            }
            else
                return Redirect("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionAndAnswerEditModel model)
        {
            //TODO: Implement this part of code to persist changes into database.
            if (ModelState.IsValid)
            {
                var qna = mapper.Map<Entities.KnowledgeBaseItem>(model);
                KnowledgeData.Edit(qna);
                return RedirectToAction("Index", "Listing");
            }
            else if (string.IsNullOrEmpty(model.Question))
                {
                    ModelState.AddModelError("Question", "error question");
                }
            else if (string.IsNullOrEmpty(model.Answer))
                {
                    ModelState.AddModelError("Answer", "Answer is required");
                }
            else if (string.IsNullOrEmpty(model.Tags))
                {
                    ModelState.AddModelError("Tags", "error Tags");
                }
            return View(model);
        }
        //agregue borrar
        public ActionResult Delete(int id){
            KnowledgeData.Delete(id);
            return RedirectToAction("Index","Listing");
        }

    }
}