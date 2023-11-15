using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class CarStatus : IFeature
    {
        public FeatureData Data { get; set; } = new FeatureData()
        {
            PageName = "CarStatus",
            Name = "Car status",
            Description = "Control information about your car",
            IsReusable = true,
            IsUsed = false
        };

        public IFeature Create()
        {
            IFeature newFeature = new CarStatus();
            newFeature.Data.IsUsed = true;

            return newFeature;
        }
    }
}
