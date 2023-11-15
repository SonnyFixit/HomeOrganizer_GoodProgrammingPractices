using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class PetStatus : IFeatureData
    {
        public string Name { get; } = "Pet status";
        public string DisplayName { get; set; } = "Pet status";

        public string Description { get; } = "Save information about your pet!";
        public bool IsUsed { get; set; } = false;
        public bool IsReusable { get; set; } = true;

        public IFeatureData Create()
        {
            return new PetStatus();
        }
    }
}
