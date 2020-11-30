using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace MapoticPoiExportTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.start();
        }
    }
}
