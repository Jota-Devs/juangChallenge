using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSchool.Models
{
    public class TagCloudModel
    {
        public int MaxCount { get; set; }
        public List<TagModel> Tags { get; set; }

        public string GetCssClass(int count)
        {
            string retCssClass = string.Empty;
            double percentage = (double)count / (double)MaxCount;
            if (percentage < 0.05)
            {
                retCssClass = "tag1";
            }
            else if (percentage < 0.15)
            {
                retCssClass = "tag2";
            }
            else if (percentage < 0.30)
            {
                retCssClass = "tag3";
            }
            else if (percentage < 0.50)
            {
                retCssClass = "tag4";
            }
            else if (percentage < 0.65)
            {
                retCssClass = "tag5";
            }
            else if (percentage < 0.90)
            {
                retCssClass = "tag6";
            }
            else
            {
                retCssClass = "tag7";
            }
            return retCssClass;
        }
    }

    public class TagModel
    {
        public string Tag { get; set; }
        public int Count { get; set; }

        public TagModel()
        {
        }
    }
}