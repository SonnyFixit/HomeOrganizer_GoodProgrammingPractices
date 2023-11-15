namespace HomeOrganizer.Models.Interfaces
{
    public interface IFeature
    {
        public FeatureData Data { get; set; }
        public IFeature Create();
    }
}
