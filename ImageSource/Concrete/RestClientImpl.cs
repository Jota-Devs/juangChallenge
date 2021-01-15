using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageSource.Concrete
{
    public sealed class RestClientImpl : IRestClient
    {
        int hits = 0;

        public int HitCounter => hits;

        public string Get(string url)
        {
            string content = string.Empty;
            WebClient myWebClient = new WebClient();
            using (Stream myStream = myWebClient.OpenRead(url))
            {
                StreamReader sr = new StreamReader(myStream);
                content = sr.ReadToEnd();
                myStream.Close();

                hits++;
            }

            return content;
        }
    }
}
