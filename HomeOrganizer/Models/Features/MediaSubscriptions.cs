using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class MediaSubscriptions : IFeature
    {
        public FeatureData Data { get; set; } = new FeatureData()
        {
            PageName = "MediaSubscriptions",
            Name = "Media subscriptions",
            Description = "Manage subcriptions for media like Netflix, HBO, Disney etc.",
            IsReusable = false,
            IsUsed = false
        };

        public IFeature Create()
        {
            IFeature newFeature = new MediaSubscriptions();
            newFeature.Data.IsUsed = true;

            return newFeature;
        }
    }
}
