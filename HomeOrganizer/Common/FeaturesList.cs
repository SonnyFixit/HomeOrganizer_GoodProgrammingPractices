using HomeOrganizer.Components.Features;
using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Common
{
    public static class FeaturesList
    {
        public static List<IFeatureTileData> CreateNewFeatures()
        {

            return new List<IFeatureTileData>()
            {
                (new MediaSubscriptions() as IFeatureTileData),
            };
        }
    }
}
