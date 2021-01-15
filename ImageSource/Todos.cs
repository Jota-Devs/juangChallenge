using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSource
{
    public class Todos
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
