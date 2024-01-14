using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudBlazor.Utilities;

namespace HomeOrganizer.Models
{
    /// <summary>
    /// Represents the data associated with a tile in the Home Organizer application.
    /// </summary>
    public class TileData
    {
        // The position of the tile in the layout, useful for ordering or arranging tiles.
        public int Position { get; set; } = 0;

        // The name given to the tile by the user, used for personalization.
        public string UserGivenName { get; set; } = "Name given by User";

        // A description provided by the user for the tile.
        public string UserGivenDescription { get; set; } = "My feature about this and that, very helpful!";

        // The icon associated with the tile, defaulting to a question mark.
        public string Icon { get; set; } = MudBlazor.Icons.Material.Filled.QuestionMark;

        // An index to reference a color from a predefined color palette for the tile.
        public int ColorIndex { get; set; } = 0;

        // Icon color

        // Tile color
    }
}
