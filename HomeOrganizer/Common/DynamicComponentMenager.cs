using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using HomeOrganizer.Models.Bases;

namespace HomeOrganizer.Common
{
    public static class DynamicComponentMenager
    {
        private static Type[] userSettingPanels = Array.Empty<Type>();
        private static Type[] featurePanels = Array.Empty<Type>();

        static DynamicComponentMenager()
        {
            string featurePanelsNamespace = "HomeOrganizer.Components.Features";
            featurePanels = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Namespace == featurePanelsNamespace).ToArray();

            string userSettingPanelsNamespace = "HomeOrganizer.Components.UserPanel";
            userSettingPanels = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Namespace == userSettingPanelsNamespace).ToArray();
        }

        public static Type? GetFeaturePanelType(FeatureBase feature)
        {
            return featurePanels.FirstOrDefault(t => t.Name.Contains(feature.FeatureData.PageName));
        }

        public static Type? GetUserPanelType(string userSetting)
        {
            return userSettingPanels.FirstOrDefault(t => t.Name.Contains(userSetting));
        }
    }
}
