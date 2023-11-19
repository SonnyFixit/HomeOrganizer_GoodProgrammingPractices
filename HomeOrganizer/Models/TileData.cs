﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudBlazor.Utilities;

namespace HomeOrganizer.Models
{
    public class TileData
    {
        public int Position { get; set; } = 0;
        public string UserGivenName { get; set; } = "Name given by User";
        public string UserGivenDescription { get; set; } = "My feature about this and that, very helpful!";
        public string Icon { get; set; } = MudBlazor.Icons.Material.Filled.QuestionMark;
        public int ColorIndex { get; set; } = 0;

        // Icon color

        // Tile color
    }
}
