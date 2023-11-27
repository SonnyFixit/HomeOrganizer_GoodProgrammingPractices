using HomeOrganizer.Common;
using HomeOrganizer.Models.Bases;

namespace HomeOrganizer.Models.Features
{
    public class MediaSubscriptions : FeatureBase
    {
        public override FeatureData FeatureData { get; set; } = new FeatureData()
        {
            PageName = "MediaSubscriptions",
            Name = "Media subscriptions",
            Description = "Manage subcriptions for media like Netflix, HBO, Disney etc.",
            IsReusable = false,
            IsUsed = false
        };
        public override TileData TileData { get; set; } = new TileData()
        {
            Icon = Constants.GetTileIcon("Payments"),
            UserGivenName = "Name for media subscriptions",
            UserGivenDescription = "Describe them",
        };

        public override FeatureBase Create()
        {
            FeatureBase newFeature = new MediaSubscriptions();
            newFeature.FeatureData.IsUsed = true;

            return newFeature;
        }
    }
}
