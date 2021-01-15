using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheSchool.Entities;
using TheSchool.Entities.Export;
using TheSchool.Models;

namespace TheSchool.Controllers
{
    public class ListingController : Controller
    {
        Services.IQueryService<KnowledgeBaseItem> KnowledgeQuery;
        Services.IExportService<QnAMakerSetting> KnowledgeQnAExport;
        readonly AutoMapper.IMapper mapper;
        public ListingController(Services.IQueryService<KnowledgeBaseItem> queryService, Services.IExportService<QnAMakerSetting> exportService)
        {
            KnowledgeQuery = queryService;
            KnowledgeQnAExport = exportService;

            //TODO: Implement mapping from Entities.KnowledgeBaseItem to QuestionAndAnswerItemModel.
            //LastUpdateOn field is set with DateTime.Now and Tags field with lowercase.
            //Also create a map from TagItem to TagModel.

            var configurationManager = new AutoMapper.MapperConfiguration(
               cfg =>
               {
                   cfg.CreateMap<KnowledgeBaseItem, QuestionAndAnswerItemModel>()
                       .ForMember(kb => kb.Question, opt => opt.MapFrom(qna => qna.Query))
                       .ForMember(kb => kb.LastUpdateOn, opt => opt.MapFrom(src => DateTime.Now))
                        .ForMember(kb => kb.Tags, opt => opt.MapFrom(qna => qna.Tags.ToLower()));

                   cfg.CreateMap<TagItem, TagModel>(); //nested
               });
            mapper = configurationManager.CreateMapper();

        }

        [HttpGet]
        public ActionResult Index(string tag = "")
        {
            //TODO: Implement the corresponding call to get all items or filtered by tag.
            //Return an instance of ListingViewModel.

           List<KnowledgeBaseItem> listaBase;
           ListingViewModel lvm = new ListingViewModel();
           if (!string.IsNullOrEmpty(tag))
            {
                listaBase = KnowledgeQuery.GetByFilter(x => x.Tags.Contains(tag));
                listaBase = listaBase.Where(x => x.Tags.Trim().Split(",").Contains(tag.Trim())).ToList();
            }
            else
            {
                listaBase = KnowledgeQuery.GetAll();
            }
            foreach (KnowledgeBaseItem i in listaBase) { 
                QuestionAndAnswerItemModel qn = mapper.Map<KnowledgeBaseItem, QuestionAndAnswerItemModel>(i);
                lvm.Questions.Add(qn);
            }
            return View(lvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult ExportQnAMaker(string fileName, string folder)
        {
            var file = string.IsNullOrEmpty(fileName) ? System.Guid.NewGuid().ToString() + ".txt" : fileName;
            var path = string.IsNullOrEmpty(folder) ? AppDomain.CurrentDomain.BaseDirectory + @"\Export\" : folder;

            var knowledgeBase = KnowledgeQuery.GetAll();
            KnowledgeQnAExport.Export(knowledgeBase, new QnAMakerSetting(path, file));

            return File(path + file, "application/text", file);
        }
    }
}