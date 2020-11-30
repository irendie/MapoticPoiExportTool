using System;
using System.Collections.Generic;
using System.Text;

namespace MapoticPoiExportTool
{
    class Controller
    {
        private LanguageManager lngMngr;

        public void start()
        {
            lngMngr = new LanguageManager();
            lngMngr.setLanguage();

            Console.WriteLine(lngMngr.lang.welcomeMessage);
            Console.WriteLine(lngMngr.lang.enterEmail);
            string email = Console.ReadLine();

            Console.WriteLine(lngMngr.lang.enterPassword);
            string password = Console.ReadLine();
            StaticLibrary.ClearLine();

            Console.WriteLine(lngMngr.lang.enterMapId);
            string mapId = Console.ReadLine();

            Console.WriteLine(lngMngr.lang.processing);

            NetworkManager nm = new NetworkManager(mapId, email, password);
            KmlGenerator kmlWriter = new KmlGenerator(nm);

            kmlWriter.processMapData();

            string fileName = "MapoticExport_Map" + mapId + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            kmlWriter.writeToKml(fileName);

            Console.WriteLine(lngMngr.lang.exportedTo + fileName + ".kml" + lngMngr.lang.exitMessage);
            Console.ReadLine();
        }
    }
}
