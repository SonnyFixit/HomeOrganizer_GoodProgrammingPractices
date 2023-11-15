using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class PetStatus : IFeature
    {
        public FeatureData FeatureData { get; set; } = new FeatureData()
        {
            PageName = "Pet",
            Name = "Pet status",
            Description = "Control information about your pet",
            IsReusable = true,
            IsUsed = false
        };
        public TileData TileData { get; set; } = new TileData();

        public IFeature Create()
        {
            IFeature newFeature = new PetStatus();
            newFeature.FeatureData.IsUsed = true;

            return newFeature;
        }
    }
}
