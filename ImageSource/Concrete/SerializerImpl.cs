using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImageSource.Concrete
{
    public class SerializerImp : ISerializer
    {
        public string ToJson<T>(T source)
        {
            //TODO: Implement a Serialize method from Object to JSON string.
            string ret = JsonSerializer.Serialize<T>(source);
            return ret;
            // throw new NotImplementedException();
        }

        public T ToObject<T>(string source)
        {

            //TODO: Implement a Deserialize method from JSON string to Object.
            //var ret = JsonSerializer.Deserialize<T>(source);
            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(source);
            return ret;
            //throw new NotImplementedException();
        }
    }
}
