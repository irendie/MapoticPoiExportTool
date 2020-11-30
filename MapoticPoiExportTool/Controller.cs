using System;
using System.Collections.Generic;
using System.Text;

namespace MapoticPoiExportTool
{
    class Controller
    {
        private LanguageManager _lngMngr;
        private NetworkManager _networkManager;
        private DataProcessor _dataProcessor;

        public void start()
        {
            _lngMngr = new LanguageManager();
            _lngMngr.setLanguage();

            _networkManager = new NetworkManager();
            _dataProcessor = new DataProcessor();

            Console.WriteLine(_lngMngr.lang.welcomeMessage);
            Console.WriteLine(_lngMngr.lang.enterEmail);
            string email = Console.ReadLine();

            Console.WriteLine(_lngMngr.lang.enterPassword);
            string password = Console.ReadLine();
            StaticLibrary.ClearLine();

            _networkManager.login(email, password);

            var maps = _dataProcessor.processMaps(_networkManager.getMapsDataObject());

            Console.WriteLine(_lngMngr.lang.enterMapId);
            Console.WriteLine(_lngMngr.lang.enterCustomMapId);

            if (maps.Count > 0)
            {
                Console.WriteLine(_lngMngr.lang.yourMaps);
                foreach (var map in maps)
                {
                    Console.WriteLine("\tID: " + map.Key + " | " + map.Value);
                }
            }
            string mapId = Console.ReadLine();

            _networkManager.setMapId(mapId);

            Console.WriteLine(_lngMngr.lang.processing);

            KmlGenerator kmlWriter = new KmlGenerator(_networkManager);

            kmlWriter.processMapData();

            string fileName = "MapoticExport_Map" + mapId + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            kmlWriter.writeToKml(fileName);

            Console.WriteLine(_lngMngr.lang.exportedTo + fileName + ".kml" + _lngMngr.lang.exitMessage);
            Console.ReadLine();
        }
    }
}
