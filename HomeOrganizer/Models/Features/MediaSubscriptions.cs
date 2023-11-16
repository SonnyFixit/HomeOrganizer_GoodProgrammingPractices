using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class MediaSubscriptions : IFeature
    {
        public FeatureData FeatureData { get; set; } = new FeatureData()
        {
            PageName = "MediaSubscriptions",
            Name = "Media subscriptions",
            Description = "Manage subcriptions for media like Netflix, HBO, Disney etc.",
            IsReusable = false,
            IsUsed = false
        };
        public TileData TileData { get; set; } = new TileData()
        {
            Icon = MudBlazor.Icons.Material.Filled.Payments,
            UserGivenName = "Name for media subscriptions",
            UserGivenDescription = "Describe them",
        };

        public IFeature Create()
        {
            IFeature newFeature = new MediaSubscriptions();
            newFeature.FeatureData.IsUsed = true;

            return newFeature;
        }
    }
}
