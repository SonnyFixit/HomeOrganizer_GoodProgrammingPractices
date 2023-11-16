using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class HouseholdBills : FeatureBase
    {
        public override FeatureData FeatureData { get; set; } = new FeatureData()
        {
            PageName = "Houshold",
            Name = "Household bills",
            Description = "Manage bills for your household such as water, electricity etc.",
            IsReusable = true,
            IsUsed = false
        };
        public override TileData TileData { get; set; } = new TileData()
        {
            Icon = MudBlazor.Icons.Material.Filled.Home,
            UserGivenName = "Your household name",
            UserGivenDescription = "How yould you describe it?",
        };

        public override FeatureBase Create()
        {
            FeatureBase newFeature = new HouseholdBills();
            newFeature.FeatureData.IsUsed = true;

            return newFeature;
        }
    }
}
