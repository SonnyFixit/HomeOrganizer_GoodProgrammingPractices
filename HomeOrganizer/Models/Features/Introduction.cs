using HomeOrganizer.Common;
using HomeOrganizer.Models.Bases;

using MongoDB.Bson.Serialization.Attributes;

namespace HomeOrganizer.Models.Features
{
    [BsonIgnoreExtraElements]
    public class Introduction : FeatureBase
    {
        public override FeatureData FeatureData { get; set; } = new FeatureData()
        {
            Name = "Introduction",
            Description = "First user's feature tile to play with and test it",
            IsReusable = false,
            IsUsed = true,
            PageName = "Introduction"
        };

        public override TileData TileData { get; set; } = new TileData()
        {
            Icon = Constants.GetTileIcon("TagFaces"),
            UserGivenName = "Your first feature",
            UserGivenDescription = "Check it! It should introduce you to our application :)",
            Position = 0,
            ColorIndex = 0,
        };

        public override FeatureBase Create()
        {
            return new Introduction();
        }

        public static FeatureBase CreateNew()
        {
            return new Introduction();
        }
    }
}
