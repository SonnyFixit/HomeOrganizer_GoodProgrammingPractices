using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class MediaSubscriptions : IFeatureTileData
    {
        public string Name { get; } = "Media subscriptions";
        public string Description { get; } = "Manage subcriptions for media like Netflix, HBO, Disney etc.";
        public bool IsUsed { get; private set; } = false;
    }
}
