using HomeOrganizer.Models.Features;
using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Common
{
    public static class FeaturesList
    {
        public static HashSet<IFeatureData> Features { get; } = new HashSet<IFeatureData>()
        {
                new MediaSubscriptions(),
                new HouseholdBills(),
                new CarStatus(),
                new PetStatus(),
                new CustomFeature(),
        };

        public static bool CheckFeatureStatus(string featureName, int userUsage)
        {
            IFeatureData? featureData = Features.FirstOrDefault(f => f.Name == featureName) ?? throw new Exception($"Cannon identify feature called {featureName}!");

            if (userUsage == 0) return true;
            if (featureData.IsReusable) return true;
            if (!featureData.IsReusable && userUsage == 0) return true;
            return false;
        }

        public static Dictionary<string, int> FeaturesUsage()
        {
            Dictionary<string, int> usage = new Dictionary<string, int>();

            foreach (var feature in Features)
            {
                usage.Add(feature.Name, 0);
            }

            return usage;
        }
    }
}
