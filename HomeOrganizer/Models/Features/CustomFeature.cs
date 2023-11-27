using HomeOrganizer.Common;
using HomeOrganizer.Models.Bases;

namespace HomeOrganizer.Models.Features
{
    public class CustomFeature : FeatureBase
    {
        public override FeatureData FeatureData { get; set; } = new FeatureData()
        {
            PageName = "Custom",
            Name = "Custom feature",
            Description = "Store your own data!",
            IsReusable = true,
            IsUsed = false
        };
        public override TileData TileData { get; set; } = new TileData()
        {
            Icon = Constants.GetTileIcon("QuestionMark"),
            UserGivenName = "Feature name",
            UserGivenDescription = "Describe purpose of feature",
        };
        public override FeatureBase Create()
        {
            FeatureBase newFeature = new CustomFeature();
            newFeature.FeatureData.IsUsed = true;

            return newFeature;
        }
    }
}
