using System;
using System.Collections.Generic;
using System.Text;

namespace MapoticPoiExportTool
{
    class LanguageManager
    {
        public Languages.ILanguage lang;

        public void setLanguage()
        {
            Console.WriteLine("Select language (EN / CZ):");
            string langString = Console.ReadLine().ToLower();
            switch (langString)
            {
                case "en":
                    lang = new Languages.EnglishLanguage();
                    break;
                case "cz":
                    lang = new Languages.CzechLanguage();
                    break;
                default:
                    lang = new Languages.EnglishLanguage();
                    break;
            }
        }


    }
}
