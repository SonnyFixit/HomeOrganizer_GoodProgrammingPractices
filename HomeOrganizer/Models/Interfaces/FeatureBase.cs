using HomeOrganizer.Models.Features;

using MongoDB.Bson.Serialization.Attributes;

namespace HomeOrganizer.Models.Interfaces
{
    [BsonDiscriminator(Required = true, RootClass = true)]
    [BsonKnownTypes(typeof(Introduction), typeof(CarStatus), typeof(PetStatus), typeof(MediaSubscriptions), typeof(HouseholdBills), typeof(CustomFeature))]
    public abstract class FeatureBase
    {
        public abstract FeatureData FeatureData { get; set; }
        public abstract TileData TileData { get; set; }

        public abstract FeatureBase Create();

        public bool Compare(FeatureBase other)
        {
            if (other == null) return false;
            return FeatureData.Name == other.FeatureData.Name && TileData.UserGivenName == other.TileData.UserGivenName;
        }
    }
}
