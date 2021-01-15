using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TheSchool.Entities;

namespace TheSchool.Data
{
    public class WisperoDbContext : System.Data.Entity.DbContext
    {
        public WisperoDbContext() : base("WisperoTestDB")
        {
            Database.SetInitializer<WisperoDbContext>(null);
        }

        public virtual IDbSet<KnowledgeBaseItem> KnowledgeBaseItems { get; set; }
        public virtual IDbSet<TagItem> TagItems { get; set; }
    }
}
