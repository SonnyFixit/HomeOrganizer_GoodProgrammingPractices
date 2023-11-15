using HomeOrganizer.Common;
using HomeOrganizer.Models.Communication;
using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.User
{
    public class UserData
    {
        public string Login { get; private set; } = "";
        public string Name { get; private set; } = "";
        public bool UseDarkTheme { get; set; } = true;

        private readonly Dictionary<string, int> featuresUsage = new Dictionary<string, int>();

        public IFeature? OpenedFeature { get; set; }
        public List<IFeature> Features { get; private set; }

        public UserData()
        {
            Features = new List<IFeature>();
            featuresUsage = FeaturesList.FeaturesUsage();
        }

        public Response AddFeature(IFeature feature)
        {
            if (feature == null)
            {
                return new Response(false, $"Feature is unavilable");
            }

            if (!featuresUsage.ContainsKey(feature.Data.Name))
            {
                return new Response(false, $"User does not contain {feature.Data.Name}");
            }

            if (!FeaturesList.CheckFeatureStatus(feature.Data.Name, featuresUsage[feature.Data.Name]))
            {
                return new Response(false, $"Cannot create {feature.Data.Name} feature.");
            }

            int featureCounter = 2;
            string userGivenName = feature.Data.UserGivenName;
            while (Features.Any(f => f.Data.UserGivenName == feature.Data.UserGivenName))
            {
                feature.Data.UserGivenName = $"{userGivenName} - {featureCounter}";
                featureCounter++;
            }

            Features.Add(feature);
            featuresUsage[feature.Data.Name]++;
            return new Response(true, $"Created {feature.Data.Name}");
        }

        public Response RemoveFeature(IFeature feature)
        {
            if (feature == null || feature.Data.Name.Length == 0)
            {
                return new Response(false, "Remove feature is unknown");
            }

            IFeature? featureToDelete = Features.FirstOrDefault(f => f.Data.Name == feature.Data.Name && f.Data.UserGivenName == feature.Data.UserGivenName);

            if (featureToDelete == null)
            {
                return new Response(false, $"Features container does not contain feature {feature.Data.Name}, {feature.Data.UserGivenName}");
            }

            featuresUsage[featureToDelete.Data.Name]--;
            Features.Remove(featureToDelete);

            return new Response(true, $"Feature {feature.Data.Name}, {feature.Data.UserGivenName} is removed");
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

            return admin;
        }
    }
}
