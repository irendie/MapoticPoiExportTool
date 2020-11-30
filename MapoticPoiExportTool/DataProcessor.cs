using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapoticPoiExportTool
{
    class DataProcessor
    {
        public Dictionary<string, string> processMaps (JObject json)
        {
            var maps = new Dictionary<string, string>();

            foreach (var map in json["maps"]["my"])
            {
                maps.TryAdd(map["id"].ToString(), map["name"].ToString());
            }

            foreach (var map in json["maps"]["followed"])
            {
                maps.TryAdd(map["id"].ToString(), map["name"].ToString());
            }

            return maps;
        }
    }
}
