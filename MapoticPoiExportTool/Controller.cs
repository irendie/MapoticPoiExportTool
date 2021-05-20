using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapoticPoiExportTool.Common;
using MapoticPoiExportTool.Common.Enum;
using MapoticPoiExportTool.Model;
using MapoticPoiExportTool.Parser;
using MapoticPoiExportTool.Service;
using Newtonsoft.Json.Linq;

namespace MapoticPoiExportTool
{
    public class Controller
    {
        private Common.Language.ILanguage _lang;
        private NetworkService _networkService;
        private MapoticJsonParser _parser;
        private ExportService _exportService;

        public Controller()
        {
            _networkService = new NetworkService();
            _parser = new MapoticJsonParser();
            _exportService = new ExportService();
        }

        public void Start()
        {
            SetLanguage();

            Console.WriteLine(_lang.welcomeMessage);

            Console.WriteLine(_lang.enterEmail);
            string email = Console.ReadLine();

            Console.WriteLine(_lang.enterPassword);
            string password = Console.ReadLine();
            ConsoleUtils.ClearLine();

            _networkService.Login(email, password);

            PrintUserMaps();

            Console.WriteLine(_lang.enterCustomMapId);
            Console.WriteLine(_lang.enterMapId);
            string mapId = Console.ReadLine();

            var outputFormat = GetOutputFormat();

            Console.WriteLine(_lang.processing);

            var mapPois = GetMapPois(mapId);

            string resultPath = ExportData(outputFormat, mapPois, mapId);

            Console.WriteLine(_lang.exportedTo + resultPath + _lang.exitMessage);

            Console.ReadLine();
        }

        private void SetLanguage()
        {
            Console.WriteLine("Select language (EN / CZ):");
            string langString = Console.ReadLine().ToLower();
            switch (langString)
            {
                case "en":
                    _lang = new Common.Language.EnglishLanguage();
                    break;
                case "cz":
                    _lang = new Common.Language.CzechLanguage();
                    break;
                default:
                    _lang = new Common.Language.EnglishLanguage();
                    break;
            }
        }

        private void PrintUserMaps ()
        {
            var mapsObject = _networkService.GetAvailableMaps();
            var maps = _parser.ParseAvailableMaps(mapsObject);

            if (maps.Count > 0)
            {
                Console.WriteLine(_lang.yourMaps);
                foreach (var map in maps)
                {
                    Console.WriteLine("\tID: " + map.Key + " | " + map.Value);
                }
            }
        }

        private List<PointOfInterestModel> GetMapPois (string mapId)
        {
            var result = new List<PointOfInterestModel>();

            var rawPoiList = _networkService.GetAvailableMapPois(mapId);
            var poiList = _parser.ParseMapPoiList(rawPoiList);

            var rawPois = new List<JObject>();

            Parallel.ForEach(poiList, poiId =>
            {
                var rawPoi = _networkService.GetPoi(mapId, poiId);
                rawPois.Add(rawPoi);
            }
            );

            Parallel.ForEach(rawPois, rawPoi =>
            {
                var poi = _parser.ParsePoi(rawPoi);
                result.Add(poi);
            }
            );

            return result;
        }

        private string GetOutputFormat ()
        {
            string result = "";

            List<string> availableFormats = new List<string>();
            foreach (string format in Enum.GetNames(typeof(OutputFileFormat)))
            {
                availableFormats.Add(format);
            }

            Console.WriteLine(_lang.ChooseExportFormat);
            Console.WriteLine("\t" + String.Join(", ", availableFormats) + "\n");
            string formatString = Console.ReadLine().ToLower();
            switch (formatString)
            {
                case "kml":
                    result = "kml";
                    break;
                default:
                    result = "kml";
                    break;
            }

            return result;
        }

        private string ExportData(string outputFormat, List<PointOfInterestModel> mapPois, string mapId)
        {
            var resultPath = "";

            string fileName = "MapoticExport_Map" + mapId + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".kml";

            switch (outputFormat)
            {
                case "kml":
                    resultPath = ExportToKml(mapPois, fileName);
                    break;
                default:
                    resultPath = ExportToKml(mapPois, fileName);
                    break;
            }

            return resultPath;
        }

        private string ExportToKml(List<PointOfInterestModel> mapPois, string fileName)
        {
            KmlSerializer serializer = new KmlSerializer();

            var serializedData = serializer.Serialize(mapPois);

            var resultPath = _exportService.ExportToKml(serializedData, fileName);

            return resultPath;
        }
    }
}
