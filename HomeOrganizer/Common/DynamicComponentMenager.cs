using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using HomeOrganizer.Models.Bases;

namespace HomeOrganizer.Common
{
    /// <summary>
    /// Manages dynamic retrieval of component types for feature panels and user setting panels.
    /// </summary>
    public static class DynamicComponentMenager
    {
        private static Type[] userSettingPanels = Array.Empty<Type>();
        private static Type[] featurePanels = Array.Empty<Type>();

        /// <summary>
        /// Static constructor to initialize and load types for feature and user setting panels.
        /// </summary>
        static DynamicComponentMenager()
        {
            // Load all class types from a specific namespace for feature panels
            string featurePanelsNamespace = "HomeOrganizer.Components.Features";
            featurePanels = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Namespace == featurePanelsNamespace).ToArray();

            // Load all class types from a specific namespace for user setting panels
            string userSettingPanelsNamespace = "HomeOrganizer.Components.UserPanel";
            userSettingPanels = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Namespace == userSettingPanelsNamespace).ToArray();
        }

        /// <summary>
        /// Retrieves the type of the feature panel associated with a specific feature.
        /// </summary>
        /// <param name="feature">The feature for which the panel type is required.</param>
        /// <returns>The Type of the feature panel if found; otherwise, null.</returns>
        public static Type? GetFeaturePanelType(FeatureBase feature)
        {
            return featurePanels.FirstOrDefault(t => t.Name.Contains(feature.FeatureData.PageName));
        }

        /// <summary>
        /// Retrieves the type of the user setting panel associated with a specific setting.
        /// </summary>
        /// <param name="userSetting">The name of the user setting for which the panel type is required.</param>
        /// <returns>The Type of the user setting panel if found; otherwise, null.</returns>
        public static Type? GetUserPanelType(string userSetting)
        {
            return userSettingPanels.FirstOrDefault(t => t.Name.Contains(userSetting));
        }
    }
}
