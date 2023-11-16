using HomeOrganizer.Common;
using HomeOrganizer.Models.Communication;
using HomeOrganizer.Models.Features;
using HomeOrganizer.Models.Interfaces;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeOrganizer.Models.User
{
    [BsonIgnoreExtraElements]
    public class UserData
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Login { get; private set; } = "";
        public string Name { get; private set; } = "";
        public bool UseDarkTheme { get; set; } = true;

        private readonly Dictionary<string, int> featuresUsage = new Dictionary<string, int>();

        public FeatureBase? OpenedFeature { get; set; }

        public List<FeatureBase> Features { get; set; }

        public UserData()
        {
            Features = new List<FeatureBase>
            {
                Introduction.CreateNew()
            };
            featuresUsage = FeaturesList.FeaturesUsage();
        }

        public void SetRegisterData(string login, string name)
        {
            this.Login = login;
            this.Name = name;
        }

        public Response AddFeature(FeatureBase feature)
        {
            if (feature == null)
            {
                return new Response(false, $"Feature is unavilable");
            }

            if (!featuresUsage.ContainsKey(feature.FeatureData.Name))
            {
                return new Response(false, $"User does not contain {feature.FeatureData.Name}");
            }

            if (!FeaturesList.CheckFeatureStatus(feature.FeatureData.Name, featuresUsage[feature.FeatureData.Name]))
            {
                return new Response(false, $"Cannot create {feature.FeatureData.Name} feature.");
            }

            int featureCounter = 2;
            string userGivenName = feature.TileData.UserGivenName;
            int max = 10;
            while (Features.Any(f => f.Compare(feature)))
            {
                feature.TileData.UserGivenName = $"{userGivenName} - {featureCounter}";
                featureCounter++;

                if (featureCounter > max)
                {
                    return new Response(false, $"Too many features named {userGivenName}");
                }
            }

            feature.TileData.Position = Features.Count;
            Features.Add(feature);
            featuresUsage[feature.FeatureData.Name]++;
            return new Response(true, $"Created {feature.FeatureData.Name}");
        }

        public Response RemoveFeature(FeatureBase feature)
        {
            if (feature == null || feature.FeatureData.Name.Length == 0)
            {
                return new Response(false, "Remove feature is unknown");
            }

            FeatureBase? featureToDelete = Features.FirstOrDefault(f => f.Compare(feature));

            if (featureToDelete == null)
            {
                return new Response(false, $"User does not contain feature {feature.FeatureData.Name}, {feature.TileData.UserGivenName} (which is weird?)");
            }

            featuresUsage[featureToDelete.FeatureData.Name]--;

            var sorted = Features.OrderBy(f => f.TileData.Position).ToList();
            int deletedIndex = sorted.IndexOf(featureToDelete);
            sorted.Remove(featureToDelete);
            for (int i = deletedIndex; i < sorted.Count; i++)
            {
                sorted[i].TileData.Position--;
            }

            Features = sorted;

            return new Response(true, $"Feature {feature.FeatureData.Name}, {feature.TileData.UserGivenName} is removed");
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
