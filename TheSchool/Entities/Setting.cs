using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSchool.Entities
{
    public abstract class Setting
    {
        public string Target { get; private set; }

        public abstract void Export(List<KnowledgeBaseItem> source);

        public Setting(string target)
        {
            Target = target;
        }
    }
}
