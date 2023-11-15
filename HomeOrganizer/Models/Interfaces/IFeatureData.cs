namespace HomeOrganizer.Models.Interfaces
{
    public interface IFeature
    {
        public FeatureData Data { get; set; }
        public IFeature Create();

        public bool Compare(IFeature other)
        {
            if (other == null) return false;
            return Data.Name == other.Data.Name && Data.UserGivenName == other.Data.UserGivenName;
        }
    }
}
