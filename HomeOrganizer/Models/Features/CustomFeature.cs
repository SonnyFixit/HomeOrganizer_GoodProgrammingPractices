using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class CustomFeature : IFeature
    {
        public FeatureData FeatureData { get; set; } = new FeatureData()
        {
            PageName = "Custom",
            Name = "Custom feature",
            Description = "Store your own data!",
            IsReusable = true,
            IsUsed = false
        };
        public TileData TileData { get; set; } = new TileData()
        {
            Icon = MudBlazor.Icons.Material.Filled.QuestionMark,
            UserGivenName = "Feature name",
            UserGivenDescription = "Describe purpose of feature",
        };
        public IFeature Create()
        {
            IFeature newFeature = new CustomFeature();
            newFeature.FeatureData.IsUsed = true;

            return newFeature;
        }
    }
}
