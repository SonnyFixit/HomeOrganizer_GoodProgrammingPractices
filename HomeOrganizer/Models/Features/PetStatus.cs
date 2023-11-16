using HomeOrganizer.Models.Bases;

namespace HomeOrganizer.Models.Features
{
    public class PetStatus : FeatureBase
    {
        public override FeatureData FeatureData { get; set; } = new FeatureData()
        {
            PageName = "Pet",
            Name = "Pet status",
            Description = "Control information about your pet",
            IsReusable = true,
            IsUsed = false
        };
        public override TileData TileData { get; set; } = new TileData()
        {
            Icon = MudBlazor.Icons.Material.Filled.Pets,
            UserGivenName = "Name of your pet",
            UserGivenDescription = "Describe your pet in few words",
        };

        public override FeatureBase Create()
        {
            FeatureBase newFeature = new PetStatus();
            newFeature.FeatureData.IsUsed = true;

            return newFeature;
        }
    }
}
