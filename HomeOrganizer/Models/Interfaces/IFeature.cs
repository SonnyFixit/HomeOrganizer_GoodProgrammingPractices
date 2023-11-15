namespace HomeOrganizer.Models.Interfaces
{
    public interface IFeature
    {
        public FeatureData FeatureData { get; set; }
        public TileData TileData { get; set; }

        public IFeature Create();

        public bool Compare(IFeature other)
        {
            if (other == null) return false;
            return FeatureData.Name == other.FeatureData.Name && TileData.UserGivenName == other.TileData.UserGivenName;
        }
    }
}
