using HomeOrganizer.Models.Features;
using HomeOrganizer.Models.Bases;

namespace HomeOrganizer.Common
{

    /// <summary>
    /// Provides a list of features available in the Home Organizer application.
    /// It includes methods to create features, check their status, and track their usage.
    /// </summary>
    public static class FeaturesList
    {
        // A collection of all available feature types in the application.
        public static HashSet<FeatureBase> Features { get; } = new HashSet<FeatureBase>()
        {
                new Introduction(),
                new MediaSubscriptions(),
                new HouseholdBills(),
                new CarStatus(),
                new PetStatus(),
                new CustomFeature(),
        };

        /// <summary>
        /// Creates a new instance of a feature based on the feature name.
        /// </summary>
        /// <param name="featureName">The name of the feature to create.</param>
        /// <returns>A new instance of the requested feature, or null if not found.</returns>
        public static FeatureBase CreateFeature(string featureName)
        {
            FeatureBase feature = Features.FirstOrDefault(f => f.FeatureData.Name == featureName);
            if (feature == null) return null;
            else return feature.Create();
        }

        /// <summary>
        /// Checks the status of a feature based on its name and user usage count.
        /// </summary>
        /// <param name="featureName">The name of the feature to check.</param>
        /// <param name="userUsage">The count of how many times the user has used the feature.</param>
        /// <returns>True if the feature is available for use, false otherwise.</returns>
        public static bool CheckFeatureStatus(string featureName, int userUsage)
        {
            FeatureBase? featureData = Features.FirstOrDefault(f => f.FeatureData.Name == featureName) ?? throw new Exception($"Cannon identify feature called {featureName}!");

            if (userUsage == 0) return true;
            if (featureData.FeatureData.IsReusable) return true;
            if (!featureData.FeatureData.IsReusable && userUsage == 0) return true;
            return false;
        }

        /// <summary>
        /// Initializes and returns a dictionary to track the usage of each feature.
        /// </summary>
        /// <returns>A dictionary with feature names as keys and usage counts as values.</returns>
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
