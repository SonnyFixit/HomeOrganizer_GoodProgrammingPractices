using MudBlazor;
using MudBlazor.Utilities;

namespace HomeOrganizer.Common
{
    public static class Constants
    {
        public static readonly Dictionary<string, string> TileIcons = new Dictionary<string, string>()
        {
            {"QuestionMark", Icons.Material.Filled.QuestionMark  },
            {"Favorite", Icons.Material.Filled.Favorite      },
            {"Star", Icons.Material.Filled.Star          },
            {"Pets", Icons.Material.Filled.Pets        },
            {"DirectionsCar", Icons.Material.Filled.DirectionsCar },
            {"Home", Icons.Material.Filled.Home       },
            {"Payments", Icons.Material.Filled.Payments      },
            {"TagFaces", Icons.Material.Filled.TagFaces      },
            {"Warning", Icons.Material.Filled.Warning       },
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

        /// <summary>
        /// False -> PaletteLight <br/>
        /// True -> PaletteDark
        /// </summary>
        public static readonly Dictionary<bool, Palette> CustomPalettes = new Dictionary<bool, Palette>()
        {
            {
                false,
                new PaletteLight()
                {
                    // Main colors
                    Primary = Colors.Blue.Lighten1,
                    Secondary = Colors.Teal.Lighten1,

                    // Theme icon color...
                    Tertiary = Colors.BlueGrey.Lighten1,

                    // Main elements
                    Background = Colors.LightBlue.Lighten4,


                    // Appbar
                    AppbarBackground = Colors.DeepOrange.Lighten2,
                    AppbarText = Colors.Shades.Black
                }
            },
            {
                true,
                new PaletteDark()
                {
                    Primary = Colors.Blue.Darken2,
                    Secondary = Colors.Teal.Darken2,

                    Tertiary = Colors.Amber.Lighten3,
                }
            }
        };


        public static readonly MudTheme CustomTheme = new MudTheme()
        {
            Palette = CustomPalettes[false],
            PaletteDark = CustomPalettes[true]
        };

    }
}
