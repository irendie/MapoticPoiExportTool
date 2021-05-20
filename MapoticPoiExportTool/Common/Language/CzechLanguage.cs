namespace MapoticPoiExportTool.Common.Language
{
    public class CzechLanguage : ILanguage
    {
        string ILanguage.welcomeMessage => "\nNeoficialni Mapotic Export Tool\nVerze 2.0.0\nPouziti na vlastni nebezpeci\nc2020 - Stepan Borek\n";

        public string enterEmail => "Zadejte email: ";

        public string enterPassword => "Zadejte heslo (zmizi z konzole po zadani): \n";

        public string enterMapId => "Zadejte ID mapy k exportu: ";

        public string yourMaps => "\nVase vlastni nebo sledovane mapy:";

        public string enterCustomMapId => "(ID jakekoliv mapy je ulozeno v cookie 'map_id' kdyz otevrete mapu v prohlizeci)";

        public string processing => "\nZpracovavam...";

        public string exportedTo => "\nHOTOVO\nExportovano do ";

        public string exitMessage => "\n\nStisknete Enter pro ukonceni programu...";

        public string ChooseExportFormat => "\nVyberte cílový formát dat: ";
    }
}
