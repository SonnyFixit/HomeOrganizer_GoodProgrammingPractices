using HomeOrganizer.Common;
using HomeOrganizer.Models.Communication;
using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.User
{
    public class UserData
    {
        public string Name { get; private set; } = "";
        public bool UseDarkTheme { get; private set; } = true;

        private Dictionary<string, int> featuresUsage = new Dictionary<string, int>();
        public List<IFeatureData> Features { get; private set; }

        public UserData()
        {
            Features = new List<IFeatureData>();
            featuresUsage = FeaturesList.FeaturesUsage();
        }

        public Response AddFeature(string featureName)
        {
            IFeatureData? featureData = FeaturesList.Features.FirstOrDefault(f => f.Name == featureName);
            if (featureData == null)
            {
                return new Response(false, $"Feature {featureName} is unavilable");
            }

            if (!featuresUsage.ContainsKey(featureData.Name))
            {
                return new Response(false, $"User does not contain {featureName}");
            }

            if (!FeaturesList.CheckFeatureStatus(featureData.Name, featuresUsage[featureData.Name]))
            {
                return new Response(false, $"Cannot create {featureName} feature.");
            }

            IFeatureData newFeature = featureData.Create();
            newFeature.IsUsed = true;

            Features.Add(newFeature);
            featuresUsage[featureData.Name]++;
            return new Response(true, $"Created {featureName}");
        }

        public List<string> GetAvailableFeatures()
        {
            List<string> features = new List<string>();
            foreach (var feature in featuresUsage)
            {
                if (FeaturesList.CheckFeatureStatus(feature.Key, feature.Value))
                {
                    features.Add(feature.Key);
                }
            }

            return features;
        }

        public static UserData CreateAdmin()
        {
            UserData admin = new UserData();
            admin.Name = "admin";
            foreach (var feature in admin.Features)
            {
                if (Random.Shared.NextDouble() < 0.99)
                {
                    feature.IsUsed = true;
                }
            }

            return admin;
        }
    }
}
