using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheSchool.Entities
{
    [Table("TagItems", Schema = "dbo")]
    public class TagItem
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public int Count { get; set; }
    }

   
}
