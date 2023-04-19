using EasyGrid.Common.Models.GPX;

namespace EasyGrid.Utils.Converters
{
    public interface IGpxToStringConverter
    {
        string ConvertToXml(Gpx gpx);
    }
}
