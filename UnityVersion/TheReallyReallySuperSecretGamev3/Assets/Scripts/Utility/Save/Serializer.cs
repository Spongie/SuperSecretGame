using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utility.Save
{
    public class Serializer
    {
        public static void Serialize(object objectToSerialize)
        {
            var settings = new JsonSerializerSettings() { ContractResolver = new SaveContractResolver() };
            var json = JsonConvert.SerializeObject(objectToSerialize, settings);

            File.WriteAllText("SaveData.ssp", json);
        }
    }
}
