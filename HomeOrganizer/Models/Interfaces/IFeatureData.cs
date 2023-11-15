namespace HomeOrganizer.Models.Interfaces
{
    public interface IFeatureData
    {
        public string Name { get; }
        public string DisplayName { get; set; }
        public string Description { get; }
        public bool IsUsed { get; set; }
        public bool IsReusable { get; set; }

        public IFeatureData Create();
    }
}
