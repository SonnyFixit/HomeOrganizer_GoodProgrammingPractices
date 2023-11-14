using HomeOrganizer.Common;
using HomeOrganizer.Models.Interfaces;

namespace HomeOrganizer.Models.User
{
    public class UserData
    {
        public string Name { get; private set; } = "";
        public bool UseDarkTheme { get; private set; } = true;

        public List<IFeatureTileData> Features { get; private set; }

        public UserData()
        {
            Features = FeaturesList.CreateNewFeatures();
        }

        public UserData(string name)
        {
            Name = name;
            Features = FeaturesList.CreateNewFeatures();
        }
    }
}
