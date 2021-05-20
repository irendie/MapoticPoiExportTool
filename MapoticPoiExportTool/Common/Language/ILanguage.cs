using System;
using System.Collections.Generic;
using System.Text;

namespace MapoticPoiExportTool.Common.Language
{
    public interface ILanguage
    {
        string welcomeMessage { get; }
        string enterEmail { get; }
        string enterPassword { get; }
        string enterMapId { get; }
        string yourMaps { get; }
        string enterCustomMapId { get; }
        string processing { get; }
        string exportedTo { get; }
        string exitMessage { get; }
        string ChooseExportFormat { get; }
    }
}
