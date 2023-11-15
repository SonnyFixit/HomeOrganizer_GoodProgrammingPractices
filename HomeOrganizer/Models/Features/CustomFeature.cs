using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class CustomFeature : IFeature
    {
        public FeatureData Data { get; set; } = new FeatureData()
        {
            PageName = "Custom",
            Name = "Custom feature",
            Description = "Store your own data!",
            IsReusable = true,
            IsUsed = false
        };

        public IFeature Create()
        {
            IFeature newFeature = new CustomFeature();
            newFeature.Data.IsUsed = true;

            return newFeature;
        }
    }
}
