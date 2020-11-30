using System;
using System.Collections.Generic;
using System.Text;

namespace MapoticPoiExportTool.Languages
{
    interface ILanguage
    {
        string welcomeMessage { get; }
        string enterEmail { get; }
        string enterPassword { get; }
        string enterMapId { get; }
        string processing { get; }
        string exportedTo { get; }
        string exitMessage { get; }
    }
}
