using System;
using System.Collections.Generic;
using System.Text;

namespace MapoticPoiExportTool.Model
{
    public class PointOfInterestModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CoordinatesModel Coordinates { get; set; }

        public Dictionary<string, string> Attributes { get; set; }

        public List<string> ImageUrls { get; set; }
    }
}
