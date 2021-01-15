using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSource
{
    public interface ISerializer
    {
        T ToObject<T>(string source);
        string ToJson<T>(T source);
    }
}
