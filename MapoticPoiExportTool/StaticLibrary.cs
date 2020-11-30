using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MapoticPoiExportTool
{
    class StaticLibrary
    {
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}
