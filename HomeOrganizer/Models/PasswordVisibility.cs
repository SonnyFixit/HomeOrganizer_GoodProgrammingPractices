using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MudBlazor;

namespace HomeOrganizer.Models
{
    /// <summary>
    /// Manages the visibility state of a password field, allowing toggling between hidden and visible states.
    /// </summary>
    public class PasswordVisibility
    {
        public bool ShowPassword { get; set; } = false;
        public InputType InputType { get; set; } = InputType.Password;
        public string Icon { get; set; } = Icons.Material.Filled.VisibilityOff;

        public void ChangeVisibility()
        {
            ShowPassword = !ShowPassword;
            if (ShowPassword)
            {
                InputType = InputType.Text;
                Icon = Icons.Material.Filled.Visibility;
            }
            else
            {
                InputType = InputType.Password;
                Icon = Icons.Material.Filled.VisibilityOff;
            }
        }
    }
}
