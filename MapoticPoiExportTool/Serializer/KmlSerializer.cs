using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using MapoticPoiExportTool.Model;
using Newtonsoft.Json.Linq;

namespace MapoticPoiExportTool
{
    public class KmlSerializer
    {
        private XNamespace xNamespace;

        public KmlSerializer()
        {
            xNamespace = "http://www.opengis.net/kml/2.2";
        }

        public XElement Serialize(List<PointOfInterestModel> pointOfInterestModels) 
        {
            XElement result = new XElement(xNamespace + "kml");

            foreach (PointOfInterestModel poi in pointOfInterestModels)
            {
                XElement newPlacemark = CreatePoiElement(poi);

                result.Add(newPlacemark);
            }

            return result;
        }

        private XElement CreatePoiElement(PointOfInterestModel poi)
        {
            XElement placemarkElement = new XElement(xNamespace + "Placemark");

            XElement nameElement = new XElement(xNamespace + "name");
            nameElement.Value = poi.Name;
            placemarkElement.Add(nameElement);

            XElement descriptionElement = new XElement(xNamespace + "description");
            descriptionElement.Value = ParseAttributesToSingleDescriptionFiled(poi.Attributes);
            placemarkElement.Add(descriptionElement);

            XElement pointElement = new XElement(xNamespace + "Point");
            XElement coordinatesElement = new XElement(xNamespace + "coordinates");
            coordinatesElement.Value = poi.Coordinates.Latitude.ToString() + ", " + poi.Coordinates.Longitude.ToString();
            pointElement.Add(coordinatesElement);
            placemarkElement.Add(pointElement);

            return placemarkElement;
        }

        private string ParseAttributesToSingleDescriptionFiled(Dictionary<string, string> attributes)
        {
            string description = "";

            List<string> allAttributeStrings = new List<string>();

            foreach (var attribute in attributes)
            {
                allAttributeStrings.Add(attribute.Key + ": " + attribute.Value);
            }

            description = String.Join("\n\n", allAttributeStrings);

            return description;
        }
    }
}
