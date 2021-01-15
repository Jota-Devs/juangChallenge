using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSchool.Entities.Export
{
    public class ExportServiceImpl<T> :
         Services.IExportService<T> where T : Setting
    {
        public ExportServiceImpl()
        {

        }

        public void Export(List<KnowledgeBaseItem> source, T settings)
        {
            settings.Export(source);
        }
    }
}
