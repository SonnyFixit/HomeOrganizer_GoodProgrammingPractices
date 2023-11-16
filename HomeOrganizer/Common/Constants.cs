namespace HomeOrganizer.Common
{
    public static class Constants
    {
        public static readonly Dictionary<string, string> TileIcons = new Dictionary<string, string>()
        {
            {"QuestionMark", MudBlazor.Icons.Material.Filled.QuestionMark  },
            {"Favorite", MudBlazor.Icons.Material.Filled.Favorite      },
            {"Star", MudBlazor.Icons.Material.Filled.Star          },
            {"Pets", MudBlazor.Icons.Material.Filled.Pets        },
            {"DirectionsCar", MudBlazor.Icons.Material.Filled.DirectionsCar },
            {"Home", MudBlazor.Icons.Material.Filled.Home       },
            {"Payments", MudBlazor.Icons.Material.Filled.Payments      },
            {"TagFaces", MudBlazor.Icons.Material.Filled.TagFaces      },
            {"Warning", MudBlazor.Icons.Material.Filled.Warning       },
        };

        public static string GetTileIcon(string name)
        {
            if (TileIcons.ContainsKey(name)) return TileIcons[name];
            return TileIcons["QuestionMark"];
        }
    }
}
