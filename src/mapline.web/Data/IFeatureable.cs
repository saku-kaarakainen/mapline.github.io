using NetTopologySuite.Features;

namespace Mapline.Web.Data
{
    public interface IFeatureable
    {
        Feature ToFeature();
    }
}
