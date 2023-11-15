using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class CarStatus : IFeature
    {
        public FeatureData FeatureData { get; set; } = new FeatureData()
        {
            PageName = "CarStatus",
            Name = "Car status",
            Description = "Control information about your car",
            IsReusable = true,
            IsUsed = false
        };

        public TileData TileData { get; set; } = new TileData();

        public IFeature Create()
        {
            IFeature newFeature = new CarStatus();
            newFeature.FeatureData.IsUsed = true;

            return newFeature;
        }
    }
}
