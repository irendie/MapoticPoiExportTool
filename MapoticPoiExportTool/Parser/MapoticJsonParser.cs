using MapoticPoiExportTool.Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MapoticPoiExportTool.Parser
{
    public class MapoticJsonParser
    {
        public Dictionary<string, string> ParseAvailableMaps(JObject json)
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

        public List<string> ParseMapPoiList(JObject json)
        {
            var result = new List<string>();

            foreach (var poi in json["features"])
            {
                string poiId = poi["properties"]["id"].ToString();

                result.Add(poiId);
            }

            return result;
        }

        public PointOfInterestModel ParsePoi (JObject rawPoi)
        {
            PointOfInterestModel result = new PointOfInterestModel();

            result.Id = rawPoi["id"].ToString();
            result.Name = rawPoi["name"].ToString();
            result.Coordinates = new CoordinatesModel()
            {
                Latitude = (decimal)rawPoi["point"]["coordinates"][0],
                Longitude = (decimal)rawPoi["point"]["coordinates"][1]
            };

            result.Attributes = new Dictionary<string, string>();

            foreach (var attribute in rawPoi["attributes_values"])
            {
                string attributeType = attribute["attribute"]["attribute_type"].ToString();

                string attributeName = attribute["attribute"]["name"].First.First.ToString();

                string attributeValue = "";

                if (attributeType == "textarea" || attributeType == "datetime" || attributeType == "number")
                {
                    attributeValue = attribute["value"].ToString();
                }
                else if (attributeType == "select" || attributeType == "multiple_select")
                {
                    foreach (var choice in attribute["value"])
                    {
                        attributeValue += "" + attribute["attribute"]["settings"]["choices"][choice.ToString()].First.First.ToString() + ", ";
                    }

                    if (attributeValue.EndsWith(", "))
                    {
                        attributeValue = attributeValue.Remove(attributeValue.Length - 2);
                    }
                }

                result.Attributes.Add(attributeName, attributeValue);
            }

            return result;
        }
    }
}
