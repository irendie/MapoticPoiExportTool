﻿namespace MapoticPoiExportTool.Common.Language
{
    public class EnglishLanguage : ILanguage
    {
        string ILanguage.welcomeMessage => "\nUnofficial Mapotic Export Tool\nVersion 2.0.0\nUse at your own risk\nc2020 - Stepan Borek\n";

        public string enterEmail => "Enter email: ";

        public string enterPassword => "Enter password (will disappear from console after entering): \n";

        public string enterMapId => "Enter map ID to export: ";

        public string yourMaps => "\nYour own or followed maps:";

        public string enterCustomMapId => "(Any map ID can be found in cookie 'map_id' when you open your map in browser)";

        public string processing => "\nProcessing...";

        public string exportedTo => "\nDONE\nExported to ";

        public string exitMessage => "\n\nPress Enter to exit the program...";

        public string ChooseExportFormat => "\nChoose desired export data format: ";
    }
}
