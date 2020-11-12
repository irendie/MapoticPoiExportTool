using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace MapoticPoiExportTool
{
    class KmlGenerator
    {
        private XElement kml;
        private XNamespace xNamespace;
        private NetworkManager nm;
        public KmlGenerator(NetworkManager netMngr)
        {
            nm = netMngr;
            xNamespace = "http://www.opengis.net/kml/2.2";
            kml = new XElement(xNamespace + "kml");
        }

        public void processMapData() 
        {
            JObject basicPoiData = nm.getMapPoiDataObject();

            foreach (var poi in basicPoiData["features"])
            {
                string poiId = poi["properties"]["id"].ToString();
                string name = poi["properties"]["name"].ToString();
                string coords = "" + (string)poi["geometry"]["coordinates"][0] + "," + (string)poi["geometry"]["coordinates"][1];

                JObject poiAdvancedDataObject = nm.getPoiAdvancedDataObject(poiId);
                string description = getDescriptionFromAdvancedPoiData(poiAdvancedDataObject);


                XElement newPlacemark = createPoiElement(name, coords, description);

                kml.Add(newPlacemark);

                
            }
        }

        public string getDescriptionFromAdvancedPoiData(JObject poiAdvancedDataObject)
        {
            string description = "";

            
            foreach (var attribute in poiAdvancedDataObject["attributes_values"])
            {
                string attributeType = attribute["attribute"]["attribute_type"].ToString();

                if (attributeType == "textarea" || attributeType == "datetime" || attributeType == "number")
                {
                    description += "" + attribute["attribute"]["name"].First.First.ToString() + ": " + attribute["value"].ToString() + "\n\n";
                } else if (attributeType == "select" || attributeType == "multiple_select") {
                    description += "" + attribute["attribute"]["name"].First.First.ToString() + ": ";

                    foreach (var choice in attribute["value"])
                    {
                        description += "" + attribute["attribute"]["settings"]["choices"][choice.ToString()].First.First.ToString() + ", ";
                    }

                    if (description.EndsWith(", "))
                    {
                        description = description.Remove(description.Length - 2);
                    }

                    description += "\n\n";
                }
            }
            
            if (description.EndsWith("\n\n"))
            {
                description = description.Remove(description.Length - 2);
            }

            return description;
        }

        private XElement createPoiElement(string name, string coordinates, string description)
        {
            XElement placemarkElement = new XElement(xNamespace + "Placemark");

            XElement nameElement = new XElement(xNamespace + "name");
            nameElement.Value = name;
            placemarkElement.Add(nameElement);

            XElement descriptionElement = new XElement(xNamespace + "description");
            descriptionElement.Value = description;
            placemarkElement.Add(descriptionElement);

            XElement pointElement = new XElement(xNamespace + "Point");
            XElement coordinatesElement = new XElement(xNamespace + "coordinates");
            coordinatesElement.Value = coordinates;
            pointElement.Add(coordinatesElement);
            placemarkElement.Add(pointElement);

            return placemarkElement;
        }

        public void writeToKml(string fileName)
        {
            using (StringWriter sw = new StringWriterWithEncoding(Encoding.UTF8))
            {
                kml.Save(sw);
                File.WriteAllText("" + fileName + ".kml", sw.ToString());
            }
        }
    }
}
