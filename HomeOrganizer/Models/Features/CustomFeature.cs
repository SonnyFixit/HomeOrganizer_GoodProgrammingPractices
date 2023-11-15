using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class CustomFeature : IFeatureData
    {
        public string Name => "Custom Feature";
        public string DisplayName { get; set; } = "Display name";

        public string Description => "Allows user to create own data";

        public bool IsUsed { get; set; } = false;
        public bool IsReusable { get; set; } = true;

        public IFeatureData Create()
        {
            return new CustomFeature();
        }
    }
}
