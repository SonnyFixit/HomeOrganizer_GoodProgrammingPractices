﻿using HomeOrganizer.Models.Interfaces;
using HomeOrganizer.Models.User;

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

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
