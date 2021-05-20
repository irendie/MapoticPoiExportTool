using MapoticPoiExportTool.Common;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace MapoticPoiExportTool.Service
{
    public class ExportService
    {
        public string ExportToKml (XElement xElement, string fileName)
        {
            string resultPath = Path.Combine(Directory.GetCurrentDirectory(), "output", fileName);

            Directory.CreateDirectory("output");

            using (StringWriter sw = new StringWriterWithEncoding(Encoding.UTF8))
            {
                xElement.Save(sw);
                File.WriteAllText(resultPath, sw.ToString());
            }

            return resultPath;
        }
    }
}
