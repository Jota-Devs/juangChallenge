using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSchool.Entities.Export
{
    public sealed class QnAMakerSetting : Setting
    {
        public string FileName { get; private set; }
        public string Path { get; private set; }
        public string FullPath { get { return string.Format($"{Path}\\{FileName}"); } }

        public QnAMakerSetting(string path, string fileName) : base("QnAMaker")
        {
            FileName = fileName;
            Path = path;
        }

        public override void Export(List<KnowledgeBaseItem> source)
        {
            //TODO: Implement this method to write a text file on the folder and with the filename specified on the constructor.
            //write a line for each item in source using the following format... string.Format($"{item.Query}\\t{item.Answer}")
            using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(this.FullPath))
                {
                    foreach (KnowledgeBaseItem item in source)
                    {

                            file.WriteLine(string.Format($"{item.Query}\\t{item.Answer}"));
                    }
            }
           // throw new NotImplementedException();
        }
    }
}
