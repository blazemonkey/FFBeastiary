using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFBestiary.Services.JSONService
{
    public class JSONNetService : IJSONService
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object instance)
        {
            return JsonConvert.SerializeObject(instance);
        }
    }
}
