using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HomeOrganizer.Common;
using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.Features
{
    public class Introduction : IFeature
    {
        public FeatureData FeatureData { get; set; } = new FeatureData()
        {
            Name = "Introduction",
            Description = "First user's feature tile to play with and test it",
            IsReusable = false,
            IsUsed = true,
            PageName = "Introduction"
        };

        public TileData TileData { get; set; } = new TileData()
        {
            Icon = Constants.TileIcons["TagFaces"],
            UserGivenName = "Your first feature",
            UserGivenDescription = "Check it! It should introduce you to our application :)",
            Position = 0,
        };

        public IFeature Create()
        {
            return new Introduction();
        }

        public static IFeature CreateNew()
        {
            return new Introduction();
        }
    }
}
