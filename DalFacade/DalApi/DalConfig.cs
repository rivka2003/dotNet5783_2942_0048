namespace DalApi;

using System.Xml.Linq;

static class DalConfig
{
    internal static string? s_dalName;
    internal static Dictionary<string, Dictionary<string, string>> s_dalPackages;

    static DalConfig()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")
            ?? throw new DO.DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DO.DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements()
            ?? throw new DO.DalConfigException("<dal-packages> element is missing");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => p.Attributes().ToDictionary(k => k.Name.LocalName, v => v.Value));
    }
}
