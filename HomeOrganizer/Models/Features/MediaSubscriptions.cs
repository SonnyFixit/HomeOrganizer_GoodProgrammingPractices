using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class MediaSubscriptions : IFeatureData
    {
        public string Name { get; } = "Media subscriptions";
        public string DisplayName { get; set; } = "Media subscriptions";

        public string Description { get; } = "Manage subcriptions for media like Netflix, HBO, Disney etc.";
        public bool IsUsed { get; set; } = false;
        public bool IsReusable { get; set; } = false;

        public IFeatureData Create()
        {
            return new MediaSubscriptions();
        }
    }
}
