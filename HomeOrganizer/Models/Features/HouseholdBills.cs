using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class HouseholdBills : IFeature
    {
        public FeatureData FeatureData { get; set; } = new FeatureData()
        {
            PageName = "Houshold",
            Name = "Household bills",
            Description = "Manage bills for your household such as water, electricity etc.",
            IsReusable = true,
            IsUsed = false
        };
        public TileData TileData { get; set; } = new TileData();

        public IFeature Create()
        {
            IFeature newFeature = new HouseholdBills();
            newFeature.FeatureData.IsUsed = true;

            return newFeature;
        }
    }
}
