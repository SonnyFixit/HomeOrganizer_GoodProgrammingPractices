using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class HouseholdBills : IFeature
    {
        public FeatureData Data { get; set; } = new FeatureData()
        {
            PageName = "Houshold",
            Name = "Household bills",
            Description = "Manage bills for your household such as water, electricity etc.",
            IsReusable = true,
            IsUsed = false
        };

        public IFeature Create()
        {
            IFeature newFeature = new HouseholdBills();
            newFeature.Data.IsUsed = true;

            return newFeature;
        }
    }
}
