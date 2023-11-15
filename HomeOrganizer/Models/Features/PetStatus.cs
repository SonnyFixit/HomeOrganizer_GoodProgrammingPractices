using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class PetStatus : IFeature
    {
        public FeatureData Data { get; set; } = new FeatureData()
        {
            PageName = "Pet",
            Name = "Pet status",
            Description = "Control information about your pet",
            IsReusable = true,
            IsUsed = false
        };

        public IFeature Create()
        {
            IFeature newFeature = new PetStatus();
            newFeature.Data.IsUsed = true;

            return newFeature;
        }
    }
}
