using EasyGrid.Common.Models.GPX;
using EasyGrid.Utils.Converters.Implementation;

namespace EasyGrid.Utils.Converters.Wrappers
{
    public class GpxToStringConverterWrapper : IGpxToStringConverter
    {
        public string ConvertToXml(Gpx gpx) => GpxToStringConverter.ConvertToXml(gpx);
    }
}
