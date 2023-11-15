using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class CarStatus : IFeatureData
    {
        public string Name { get; } = "Car status";
        public string DisplayName { get; set; } = "Car status";
        public string Description { get; } = "Control your car!";
        public bool IsUsed { get; set; } = false;
        public bool IsReusable { get; set; } = true;

        public IFeatureData Create()
        {
            return new CarStatus();
        }
    }
}
