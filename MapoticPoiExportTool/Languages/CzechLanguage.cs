using System;
using System.Collections.Generic;
using System.Text;

namespace MapoticPoiExportTool.Languages
{
    class CzechLanguage : ILanguage
    {
        string ILanguage.welcomeMessage => "Neoficialni Mapotic Export Tool\nVerze 1.1.0\nPouziti na vlastni nebezpeci\nc2020 - Stepan Borek";

        public string enterEmail => "Zadejte email: ";

        public string enterPassword => "Zadejte heslo (zmizi z konzole po zadani): \n";

        public string enterMapId => "\nTo get you map ID open the map in your browser and find cookie with name 'map_id'\nYou can get the cookie by many ways - one of them is running 'alert(document.cookie)' in console (F12)\nEnter map ID: ";

        public string processing => "\nPracuji...";

        public string exportedTo => "\nHOTOVO\nExportovano do ";

        public string exitMessage => "\n\nStisknete Enter pro ukonceni programu...";
    }
}
