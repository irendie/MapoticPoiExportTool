using System;
using System.Collections.Generic;
using System.Text;

namespace MapoticPoiExportTool.Languages
{
    class EnglishLanguage : ILanguage
    {
        string ILanguage.welcomeMessage => "Unofficial Mapotic Export Tool\nVersion 1.1.0\nUse at your own risk\nc2020 - Stepan Borek";

        public string enterEmail => "Enter email: ";

        public string enterPassword => "Enter password (will disappear from console after entering): \n";

        public string enterMapId => "\nTo get you map ID open the map in your browser and find cookie with name 'map_id'\nYou can get the cookie by many ways - one of them is running 'alert(document.cookie)' in console (F12)\nEnter map ID: ";

        public string processing => "\nProcessing...";

        public string exportedTo => "\nDONE\nExported to ";

        public string exitMessage => "\n\nPress Enter to exit the program...";
    }
}
