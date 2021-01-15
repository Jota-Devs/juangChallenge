using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheSchool.Data.Helpers;
using TheSchool.Entities;
using TheSchool.Models;

namespace TheSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Services.IDataService<KnowledgeBaseItem> KnowledgeBaseData;
        Services.IQueryService<KnowledgeBaseItem> KnowledgeBaseQuery;
        private readonly AutoMapper.IMapper mapper;
        public HomeController(ILogger<HomeController> logger, Services.IDataService<KnowledgeBaseItem> dataService, Services.IQueryService<KnowledgeBaseItem> queryService)
        {
            _logger = logger;
            KnowledgeBaseData = dataService;
            KnowledgeBaseQuery = queryService;

            //TODO: Implement mapping from QuestionAndAnswerModel to Entities.KnowledgeBaseItem.
            //LastUpdateOn field is set with DateTime.Now and Tags field with lowercase.
            //Also create a map from TagItem to TagModel.
            //Use "mapper" attribute which is already defined. More information: https://docs.automapper.org/en/latest/Getting-started.html.

            var configurationManager = new AutoMapper.MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<QuestionAndAnswerModel, KnowledgeBaseItem>()
                        .ForMember(kb => kb.Query, opt => opt.MapFrom(qna => qna.Question))
                        .ForMember(kb => kb.LastUpdateOn, opt => opt.MapFrom(src => DateTime.Now))
                        .ForMember(kb => kb.Tags, opt => opt.MapFrom(qna => qna.Tags.ToLower()));
                   
                    cfg.CreateMap<TagItem, TagModel>();
                });
            mapper = configurationManager.CreateMapper();            
        }

        public ActionResult Index()
        {
            //TODO: Return a view "Index" with all the required information for the nested views.
            //You need to call TagHelper.Process as shown below in order to populate the object "HomeViewModel".
            //model.Tags = new TagCloudModel() { Tags = mapper.Map<List<TagModel>>(tagList), MaxCount = tagMaxCount }; CODIGO ARTURO
            int max;
            var tagItems = TagHelper.Process(KnowledgeBaseQuery, out max);
            TagCloudModel tcm = new TagCloudModel(); 
            tcm.Tags = new List<TagModel>();
            foreach (var i in tagItems) 
            {
                var tag = mapper.Map<TagItem, TagModel>(i);
                    tcm.Tags.Add(tag);
            }
            tcm.MaxCount = max;
            var model = new HomeViewModel
            {
                QA = new QuestionAndAnswerModel(),
                Tags = tcm
            };
            return View(model); 
        }

        public ActionResult Entry()
        {
            //TODO: Return partival view "Entry";
            return PartialView("Entry");
        }
        [HttpGet]
        public ActionResult TagCloud()
        {
            //TODO: Return partival view "TagCloud" with an instance of TagCloudviewModel.
            //You need to call TagHelper.Process as shown below.
            int max;
            var tagItems = TagHelper.Process(KnowledgeBaseQuery, out max);  
            TagCloudModel tcm = new TagCloudModel(); 
            tcm.Tags = new List<TagModel>();
            foreach (var i in tagItems) 
            {
                var tag = mapper.Map<TagItem, TagModel>(i);
                tcm.Tags.Add(tag);
            }
            tcm.MaxCount = max;
            return PartialView(tcm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(QuestionAndAnswerModel model)
        {
            //TODO: Return partial view "Index" to reload the page.
            //If model is valid then persists the new entry on DB. Make sure  data changes are committed.
           
            ModelStateDictionary ms = new ModelStateDictionary();
            if (string.IsNullOrEmpty(model.Question))
            {
                ms.AddModelError("Question", "error question");  
            }
            else if (string.IsNullOrEmpty(model.Answer))
            {
                ms.AddModelError("Answer", "Answer is required");
            }
            else if (string.IsNullOrEmpty(model.Tags))
            {
                ms.AddModelError("Tags", "error Tags");
            }
            else if (ms.IsValid)
            {
                var newQuestion = mapper.Map<KnowledgeBaseItem>(model);
                KnowledgeBaseData.Add(newQuestion); 
                KnowledgeBaseData.CommitChanges();
                model = new QuestionAndAnswerModel(); 
                return RedirectToAction("Index",model);
            }
            var modelH = new HomeViewModel
            {
                QA = model,
                Tags = new TagCloudModel()
            };
            return RedirectToAction("Index", modelH);       
        }
       
        
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
