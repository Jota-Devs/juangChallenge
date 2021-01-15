using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSchool.Entities;

namespace TheSchool.Data.Helpers
{
    public static class TagHelper
    {
        public static List<Entities.TagItem> Process(Services.IQueryService<Entities.KnowledgeBaseItem> knowledgeService, out int tagMaxCount)
        {
            var sourceItems = knowledgeService.GetAll();
            return Process(sourceItems, out tagMaxCount);
        }

        public static List<Entities.TagItem> Process(List<Entities.KnowledgeBaseItem> items, out int tagMaxCount)
        {
            //TODO: Based on the list of items you need to rank tags dynamically. 
            //Also, return the max value that will be used for Tag Cloud control.

            List<TagItem> ret = new List<TagItem>();
            tagMaxCount = 0;
            foreach (var i in items)
            {
                var arrString = i.Tags.Split(",");
                foreach(var x in arrString)
                {
                    TagItem newTag = new TagItem
                    {
                        Tag = x.Trim()
                    };
                    if (!ret.Exists(x => x.Tag == newTag.Tag))
                    {
                        newTag.Count = 1;
                        ret.Add(newTag);
                    }
                    else
                    {
                        var sum = ret.FirstOrDefault(x => x.Tag == newTag.Tag);
                        sum.Count += 1;
                    }
                }
                tagMaxCount = ret.Count;
            }
            return ret;
        }

    }

}
