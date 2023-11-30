using HomeOrganizer.Models.Bases;
using HomeOrganizer.Models.User;

using MyWebsiteBlazor.Data.Database;

namespace HomeOrganizer.Services
{
    // UserService.cs
    public class UserService
    {
        private UserData? loggedUser;
        public FeatureBase? DraggedFeatureTile { get; set; }
        public bool UnloggedDarkTheme = false;

        public UserData? LoggedUser
        {
            get => loggedUser;
            private set { loggedUser = value; }
        }

        public event Action? OnChange;
        public event Func<Task>? AsyncOnChange;

        public async Task UpdateUser(UserData user)
        {
            LoggedUser = user;

            if (loggedUser != null)
            {
                await DbHandler.UpdateUser(loggedUser);
                UnloggedDarkTheme = loggedUser.UseDarkTheme;
            }
            OnChange?.Invoke();
            if (AsyncOnChange != null)
                await AsyncOnChange.Invoke();
        }
    }
}
