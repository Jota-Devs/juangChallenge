using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSchool.Models
{
    public class ConversationModel
    {
        public string ConversationId { get; set; }
        public List<ConversationItemModel> Messages { get; set; }

        public ConversationModel()
        {
            Messages = new List<ConversationItemModel>();
        }
    }

    public class ConversationItemModel
    {
        [JsonProperty(PropertyName = "questions")]
        public List<string> Questions { get; set; }
        [JsonProperty(PropertyName = "question")]
        public string Question { get; set; }
        [JsonProperty(PropertyName = "answer")]
        public string Answer { get; set; }
        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }
    }

    public class QnAResponse
    {
        public List<ConversationItemModel> Answers { get; set; }
    }
}