using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace MapoticPoiExportTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Unofficial Mapotic Export Tool\nVersion 1.0.0\nUse at your own risk\n\u00a92020 - Stepan Borek\n");
            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Enter password (will disappear from console after entering): \n");
            string password = Console.ReadLine();
            ClearLine();

            Console.WriteLine("\nTo get you map ID open the map in your browser and find cookie with name 'map_id'\nYou can get the cookie by many ways - one of them is running 'alert(document.cookie)' in console (F12)\nEnter map ID: ");
            string mapId = Console.ReadLine();

            Console.WriteLine("\nProcessing...");

            NetworkManager nm = new NetworkManager(mapId, email, password);
            KmlGenerator kmlWriter = new KmlGenerator(nm);

            kmlWriter.processMapData();

            string fileName = "MapoticExport_Map" + mapId + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            kmlWriter.writeToKml(fileName);

            Console.WriteLine("\nDONE\nExported to " + fileName + ".kml\n\nPress Enter to exit the program...");
            Console.ReadLine();
        }

        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
