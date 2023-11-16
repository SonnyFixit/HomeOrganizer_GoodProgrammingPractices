using HomeOrganizer.Models.Bases;
using HomeOrganizer.Models.User;

using MyWebsiteBlazor.Data.Database;

namespace HomeOrganizer.Services
{
    // UserService.cs
    public class UserService
    {
        private UserData? loggedUser;
        public FeatureBase DraggedFeatureTile { get; set; }

        public UserData? LoggedUser
        {
            get => loggedUser;
            set
            {
                loggedUser = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged()
        {
            DatabaseHandlerMongoDB.UpdateUser(loggedUser);
            OnChange?.Invoke();
        }
    }
}
