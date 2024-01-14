using HomeOrganizer.Models.Bases;
using HomeOrganizer.Models.User;

using MyWebsiteBlazor.Data.Database;

namespace HomeOrganizer.Services
{
    /// <summary>
    /// Service for managing user-specific functionalities such as user data and preferences.
    /// </summary>
    // UserService.cs
    public class UserService
    {
        // Currently logged-in user's data.
        private UserData? loggedUser;

        // Represents the feature tile currently being dragged by the user.
        public FeatureBase? DraggedFeatureTile { get; set; }

        // Preference for dark theme when no user is logged in.
        public bool UnloggedDarkTheme = false;

        // Provides access to the currently logged-in user's data.
        public UserData? LoggedUser
        {
            get => loggedUser;
            private set { loggedUser = value; }
        }

        // Event to notify when there's a change in the user's data.
        public event Action? OnChange;

        // Asynchronous event to notify when there's a change in the user's data.
        public event Func<Task>? AsyncOnChange;

        /// <summary>
        /// Updates the currently logged-in user's data and notifies any subscribers of the change.
        /// </summary>
        /// <param name="user">The updated user data.</param>
        public async Task UpdateUser(UserData user)
        {
            LoggedUser = user;

            // Update the user in the database and sync dark theme preference.
            if (loggedUser != null)
            {
                await DbHandler.UpdateUser(loggedUser);
                UnloggedDarkTheme = loggedUser.UseDarkTheme;
            }

            // Invoke the change notification events.
            OnChange?.Invoke();
            if (AsyncOnChange != null)
                await AsyncOnChange.Invoke();
        }
    }
}
