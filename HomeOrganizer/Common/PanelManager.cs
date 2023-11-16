using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using HomeOrganizer.Models.Bases;

namespace HomeOrganizer.Common
{
    public static class PanelManager
    {
        private static Type[] availablePanels = Array.Empty<Type>();

        static PanelManager()
        {
            string nspace = "HomeOrganizer.Components.Features";
            availablePanels = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Namespace == nspace).ToArray();
        }

        public static Type? GetPanelType(FeatureBase feature)
        {
            return availablePanels.FirstOrDefault(t => t.Name.Contains(feature.FeatureData.PageName));
        }
    }
}
