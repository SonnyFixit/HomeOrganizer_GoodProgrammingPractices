using HomeOrganizer.Models.Features;
using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Common
{
    public static class FeaturesList
    {
        public static HashSet<FeatureBase> Features { get; } = new HashSet<FeatureBase>()
        {
                new Introduction(),
                new MediaSubscriptions(),
                new HouseholdBills(),
                new CarStatus(),
                new PetStatus(),
                new CustomFeature(),
        };

        public static FeatureBase CreateFeature(string featureName)
        {
            FeatureBase feature = Features.FirstOrDefault(f => f.FeatureData.Name == featureName);
            if (feature == null) return null;
            else return feature.Create();
        }

        public static bool CheckFeatureStatus(string featureName, int userUsage)
        {
            FeatureBase? featureData = Features.FirstOrDefault(f => f.FeatureData.Name == featureName) ?? throw new Exception($"Cannon identify feature called {featureName}!");

            if (userUsage == 0) return true;
            if (featureData.FeatureData.IsReusable) return true;
            if (!featureData.FeatureData.IsReusable && userUsage == 0) return true;
            return false;
        }

        public static Dictionary<string, int> FeaturesUsage()
        {
            Dictionary<string, int> usage = new Dictionary<string, int>();

            foreach (var feature in Features)
            {
                usage.Add(feature.FeatureData.Name, 0);
            }

            return usage;
        }
    }
}
