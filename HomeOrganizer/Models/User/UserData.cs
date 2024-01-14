using HomeOrganizer.Common;
using HomeOrganizer.Models.Bases;
using HomeOrganizer.Models.Communication;
using HomeOrganizer.Models.Features;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using MyWebsiteBlazor.Data.Database;

namespace HomeOrganizer.Models.User
{
    [BsonIgnoreExtraElements]
    public class UserData
    {

        /// <summary>
        /// Represents the data associated with a user in the Home Organizer application.
        /// This includes credentials, personal information, preferences, and features used.
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        public UserCredentials Credentials { get; private set; } = new UserCredentials("Default", "Default");
        public string Avatar { get; private set; } = "images/DefaultUser.png";
        public string Name { get; private set; } = "";
        public string Email { get; private set; } = "";
        public bool UseDarkTheme { get; set; } = false;
        public bool RightUserPanelNavigation { get; set; } = false;

        // Dictionary tracking usage count of different features.
        private Dictionary<string, int> featuresUsage = new Dictionary<string, int>();

        // The feature currently opened by the user.
        [BsonIgnore]
        public FeatureBase? OpenedFeature { get; set; }

        // List of features available to the user.
        public List<FeatureBase> Features { get; set; }

        /// <summary>
        /// Constructor for UserData. Initializes the list of features and sets up default usage.
        /// </summary>
        public UserData()
        {
            Features = new List<FeatureBase>
            {
                Introduction.CreateNew()
            };
            featuresUsage = FeaturesList.FeaturesUsage();
            featuresUsage[Features[0].FeatureData.Name] += 2; // Prevent from creating new Introduction tiles
        }


        /// <summary>
        /// Updates the user's name.
        /// </summary>
        /// <param name="name">New name for the user.</param>
        public void UpdateName(string name)
        {
            if (!string.IsNullOrEmpty(name) && Name != name)
            {
                Name = name;
            }
        }

        /// <summary>
        /// Updates the user's email address.
        /// </summary>
        /// <param name="email">New email address for the user.</param>
        public void UpdateEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                Email = email;
            }
        }

        /// <summary>
        /// Updates the user's avatar image.
        /// </summary>
        /// <param name="base64Image">Base64 string of the new image.</param>
        /// <returns>True if the image was successfully updated, otherwise false.</returns>
        /// 
        public bool UpdateImage(string base64Image)
        {
            if (string.IsNullOrEmpty(base64Image)) return false;
            Avatar = $"data:image/png;base64, {base64Image}";
            return true;
        }

        /// <summary>
        /// Resets the user's password.
        /// </summary>
        /// <param name="newPassword">New password for the user.</param>
        public void ResetPassword(string newPassword)
        {
            Credentials.ResetPassword(newPassword);
        }

        /// <summary>
        /// Sets the registration data for the user including login, password, email, and name.
        /// </summary>
        /// <param name="login">Login ID.</param>
        /// <param name="password">Password.</param>
        /// <param name="email">Email address.</param>
        /// <param name="name">User's name.</param>
        public void SetRegisterData(string login, string password, string email, string name)
        {
            Credentials = new UserCredentials(login, password);
            Email = email;
            Name = name;
        }

        /// <summary>
        /// Adds a new feature to the user's list of features.
        /// </summary>
        /// <param name="feature">The feature to add.</param>
        /// <returns>A response indicating success or failure with a message.</returns>
        public async Task<Response> AddFeature(FeatureBase feature)
        {
            if (feature == null)
            {
                return new Response(false, $"Feature is unavilable");
            }

            if (!featuresUsage.ContainsKey(feature.FeatureData.Name))
            {
                return new Response(false, $"User does not contain {feature.FeatureData.Name}");
            }

            if (!FeaturesList.CheckFeatureStatus(feature.FeatureData.Name, featuresUsage[feature.FeatureData.Name]))
            {
                return new Response(false, $"Cannot create {feature.FeatureData.Name} feature.");
            }

            int featureCounter = 2;
            string userGivenName = feature.TileData.UserGivenName;
            int max = 10;
            while (Features.Any(f => f.Compare(feature)))
            {
                feature.TileData.UserGivenName = $"{userGivenName} - {featureCounter}";
                featureCounter++;

                if (featureCounter > max)
                {
                    return new Response(false, $"Too many features named {userGivenName}");
                }
            }

            feature.TileData.Position = Features.Count;

            UserData? latestData = await DbHandler.GetUserByLogin(Credentials.Login);
            if (latestData == null)
            {
                return new Response(false, $"User is somehow null");
            }

            featuresUsage = latestData.featuresUsage;
            Features = latestData.Features;

            Features.Add(feature);
            featuresUsage[feature.FeatureData.Name]++;

            return new Response(true, $"Created {feature.FeatureData.Name}");
        }

        /// <summary>
        /// Updates an existing feature in the user's list.
        /// </summary>
        /// <param name="feature">The feature to update.</param>
        /// <returns>A response indicating success or failure with a message.</returns>
        public async Task<Response> UpdateFeature(FeatureBase feature)
        {
            if (feature == null || feature.FeatureData.Name.Length == 0)
            {
                return new Response(false, "Edited feature is unknown");
            }

            int updateFeatureIndex = Features.FindIndex(f => f.Compare(feature));
            Features[updateFeatureIndex] = feature;

            return new Response(true, $"Feature {feature.FeatureData.Name}, {feature.TileData.UserGivenName} updated.");
        }

        /// <summary>
        /// Removes a feature from the user's list.
        /// </summary>
        /// <param name="feature">The feature to remove.</param>
        /// <returns>A response indicating success or failure with a message.</returns>
        public async Task<Response> RemoveFeature(FeatureBase feature)
        {
            if (feature == null || feature.FeatureData.Name.Length == 0)
            {
                return new Response(false, "Remove feature is unknown");
            }

            FeatureBase? featureToDelete = Features.FirstOrDefault(f => f.Compare(feature));

            if (featureToDelete == null)
            {
                return new Response(false, $"User does not contain feature {feature.FeatureData.Name}, {feature.TileData.UserGivenName} (which is weird?)");
            }

            UserData? latestData = await DbHandler.GetUserByLogin(Credentials.Login);
            if (latestData == null)
            {
                return new Response(false, $"User is somehow null");
            }

            featuresUsage = latestData.featuresUsage;
            Features = latestData.Features;

            featuresUsage[featureToDelete.FeatureData.Name]--;

            var sorted = Features.OrderBy(f => f.TileData.Position).ToList();
            int deletedIndex = featureToDelete.TileData.Position;
            sorted.RemoveAt(deletedIndex);
            for (int i = deletedIndex; i < sorted.Count; i++)
            {
                sorted[i].TileData.Position--;
            }

            Features = sorted;

            return new Response(true, $"Feature {feature.FeatureData.Name}, {feature.TileData.UserGivenName} is removed");
        }

        /// <summary>
        /// Retrieves a list of features available to the user.
        /// </summary>
        /// <returns>List of available feature names.</returns>
        public List<string> GetAvailableFeatures()
        {
            List<string> features = new List<string>();
            foreach (var feature in featuresUsage)
            {
                if (FeaturesList.CheckFeatureStatus(feature.Key, feature.Value))
                {
                    features.Add(feature.Key);
                }
            }

            return features;
        }

        /// <summary>
        /// Creates a new UserData instance with admin privileges.
        /// </summary>
        /// <returns>New instance of UserData with admin privileges.</returns>
        public static UserData CreateAdmin()
        {
            UserData admin = new UserData();
            admin.Name = "admin";

            return admin;
        }
    }
}
