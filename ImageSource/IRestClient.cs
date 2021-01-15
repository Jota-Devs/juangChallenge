using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSource
{
    public interface IRestClient
    {
        string Get(string url);

        int HitCounter { get; }
    }
}
