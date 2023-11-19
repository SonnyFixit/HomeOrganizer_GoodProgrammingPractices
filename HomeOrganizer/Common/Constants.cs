using MudBlazor.Utilities;

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

        public static Dictionary<bool, MudColor[]> TileColorPalettes { get; } = new Dictionary<bool, MudColor[]>()
        {
            {false, new MudColor[]
                {
                    "#BEFFE3", "#A5D9FF", "#FFC6C6", "#E9CEFF","#FCFCFC",
                    "#8AFAC9", "#6FC2FF", "#FF9898", "#D7ADFA","#DBDBDB",
                    "#3AE99C", "#3A9FE9", "#F56767", "#BB8AE3","#C6C6C6",
                    "#00BA71", "#007EC5", "#CF4549", "#9768BE","#A8A8A8",
                }
            },
            {true, new MudColor[]
                {
                    "#008C48", "#005FA3", "#A91F2E", "#74489B","#7C7C7C",
                    "#006022", "#004281", "#830014", "#522979","#5C5C5C",
                    "#003800", "#062D51", "#600000", "#310A58","#3E3E3E",
                    "#002B00", "#03203C", "#260101", "#1D0139","#232323",
                }
            }
        };
    }
}
