using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheSchool.Entities
{
    [Table("KnowledgeBaseItems", Schema = "dbo")]
    public class KnowledgeBaseItem
    {
        public int Id { get; set; }
        public string Query { get; set; }
        public string Answer { get; set; }
        public string Tags { get; set; }
        public DateTime LastUpdateOn { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
