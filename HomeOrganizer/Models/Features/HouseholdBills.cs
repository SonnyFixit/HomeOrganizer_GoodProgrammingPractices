using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class HouseholdBills : IFeatureData
    {
        public string Name { get; } = "Household bills";
        public string DisplayName { get; set; } = "Household bills";

        public string Description { get; } = "Manage bills for your household such as water, electricity etc.";
        public bool IsUsed { get; set; } = false;
        public bool IsReusable { get; set; } = false;

        public IFeatureData Create()
        {
            return new HouseholdBills();
        }
    }
}
