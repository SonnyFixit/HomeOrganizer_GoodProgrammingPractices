using HomeOrganizer.Models.Features;

using MongoDB.Bson.Serialization.Attributes;

namespace HomeOrganizer.Models.Bases
{

    /// <summary>
    /// Abstract base class for different feature modules in the Home Organizer application.
    /// This class defines the basic structure and functionalities that all feature modules must implement.
    /// Known types include Introduction, CarStatus, PetStatus, MediaSubscriptions, HouseholdBills, and CustomFeature.
    /// </summary>
    [BsonDiscriminator(Required = true, RootClass = true)]
    [BsonKnownTypes(typeof(Introduction), typeof(CarStatus), typeof(PetStatus), typeof(MediaSubscriptions), typeof(HouseholdBills), typeof(CustomFeature))]
    public abstract class FeatureBase
    {
        // Abstract property to get or set the data specific to the feature.
        public abstract FeatureData FeatureData { get; set; }
        
        // Abstract property to get or set the tile data for the feature.
        public abstract TileData TileData { get; set; }

        /// <summary>
        /// Abstract method to create an instance of the feature.
        /// Each derived class must provide its own implementation of this method.
        /// </summary>
        /// <returns>A new instance of the specific feature.</returns>
        public abstract FeatureBase Create();

        /// <summary>
        /// Compares this feature with another feature to determine if they are the same.
        /// Comparison is based on the feature name and user-given name of the tile.
        /// </summary>
        /// <param name="other">The other feature to compare with.</param>
        /// <returns>True if both features are the same; otherwise, false.</returns>
        public bool Compare(FeatureBase other)
        {
            if (other == null) return false;
            return FeatureData.Name == other.FeatureData.Name && TileData.UserGivenName == other.TileData.UserGivenName;
        }
    }
}
