﻿using HomeOrganizer.Models.Features;
using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Common
{
    public static class FeaturesList
    {
        public static HashSet<IFeature> Features { get; } = new HashSet<IFeature>()
        {
                new MediaSubscriptions(),
                new HouseholdBills(),
                new CarStatus(),
                new PetStatus(),
                new CustomFeature(),
        };

        public static IFeature GetFeature(string featureName)
        {
            return Features.FirstOrDefault(f => f.Data.Name == featureName);
        }

        public static bool CheckFeatureStatus(string featureName, int userUsage)
        {
            IFeature? featureData = Features.FirstOrDefault(f => f.Data.Name == featureName) ?? throw new Exception($"Cannon identify feature called {featureName}!");

            if (userUsage == 0) return true;
            if (featureData.Data.IsReusable) return true;
            if (!featureData.Data.IsReusable && userUsage == 0) return true;
            return false;
        }

        public static Dictionary<string, int> FeaturesUsage()
        {
            Dictionary<string, int> usage = new Dictionary<string, int>();

            foreach (var feature in Features)
            {
                usage.Add(feature.Data.Name, 0);
            }

            return usage;
        }
    }
}
