using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSchool.Entities;

namespace TheSchool.Services
{
    public interface IExportService<T> where T : Setting
    {
        void Export(List<Entities.KnowledgeBaseItem> source, T settings);
    }
}
