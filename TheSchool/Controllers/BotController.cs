using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TheSchool.Models;

namespace TheSchool.Controllers
{
    public class BotController : Controller
    {
        [HttpGet]
        public ActionResult Conversation()
        {
            return View(new ConversationModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Conversation(string txtYourQuestion)
        {
            ConversationModel model = new ConversationModel();
            try
            {
                if (txtYourQuestion != null)
                {
                    var conversation = await QueryQnABot(txtYourQuestion);
                    model.Messages.Insert(0, conversation);
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error: " + ex);
                return View(model);
            }
        }

        private static async Task<ConversationItemModel> QueryQnABot(string Query)
        {
            ConversationItemModel QnAQueryResult = new ConversationItemModel();
            using (System.Net.Http.HttpClient client =
                new System.Net.Http.HttpClient())
            {
                string RequestURI = String.Format("{0}{1}",
                    "https://wispero.azurewebsites.net/qnamaker",
                    "/knowledgebases/0e778adf-9dad-4c40-8199-78cf7d3a69ca/generateAnswer");

                var httpContent =
                    new StringContent($"{{\"question\": \"{Query}\"}}",
                    Encoding.UTF8, "application/json");

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, RequestURI);
                httpRequestMessage.Headers.Add("Authorization", "EndpointKey ba505028-2b0b-4630-a8e5-d32a04efdb6a");
                httpRequestMessage.Content = httpContent;
                var msg = await client.SendAsync(httpRequestMessage);
                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse =
                        await msg.Content.ReadAsStringAsync();
                    var response =
                        JsonConvert.DeserializeObject<QnAResponse>(JsonDataResponse);

                    QnAQueryResult.Question = Query;
                    QnAQueryResult.Answer = response.Answers.FirstOrDefault()?.Answer;
                }
            }
            return QnAQueryResult;
        }
    }
}