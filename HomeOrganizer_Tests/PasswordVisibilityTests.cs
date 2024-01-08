using Xunit;
using HomeOrganizer.Models;
using MudBlazor;

// Description:
// This unit test class is designed to test the behavior of the PasswordVisibility class, which is responsible for managing password visibility in a user interface.
// The PasswordVisibility class toggles between displaying a password in plain text and hiding it, and it updates the associated input type and icon accordingly.

public class PasswordVisibilityTests
{
    [Fact]
    public void ChangeVisibility_ShouldEnablePasswordVisibility_WhenInitiallyHidden()
    {
        // Arrange

        // Create an instance of the PasswordVisibility class to be tested
        var passwordVisibilityHandler = new PasswordVisibility();

        // Act

        // Call the ChangeVisibility method to toggle password visibility
        passwordVisibilityHandler.ChangeVisibility();

        // Assert

        // Verify that the ShowPassword property is set to true, indicating that the password is visible
        Assert.True(passwordVisibilityHandler.ShowPassword, "Password should be visible after first toggle.");
    }

    [Fact]
    public void ChangeVisibility_ShouldSetInputTypeToText_WhenPasswordIsVisible()
    {
        // Arrange

        // Create an instance of the PasswordVisibility class to be tested
        var passwordVisibilityHandler = new PasswordVisibility();

        // Act

        // Call the ChangeVisibility method to toggle password visibility
        passwordVisibilityHandler.ChangeVisibility();

        // Assert

        // Verify that the InputType property is set to InputType.Text, indicating that the input type is set to text when the password is visible
        Assert.True(passwordVisibilityHandler.InputType == InputType.Text, "Input type should be text when password is visible.");
    }

    [Fact]
    public void ChangeVisibility_ShouldDisplayVisibilityIcon_WhenPasswordIsVisible()
    {
        // Arrange

        // Create an instance of the PasswordVisibility class to be tested
        var passwordVisibilityHandler = new PasswordVisibility();

        // Act

        // Call the ChangeVisibility method to toggle password visibility
        passwordVisibilityHandler.ChangeVisibility();

        // Assert

        // Verify that the Icon property is set to Icons.Material.Filled.Visibility, indicating that the visibility icon is displayed when the password is visible
        Assert.True(passwordVisibilityHandler.Icon == Icons.Material.Filled.Visibility, "Visibility icon should be displayed when password is visible.");
    }

    [Fact]
    public void ChangeVisibility_ShouldDisablePasswordVisibility_WhenInitiallyVisible()
    {
        // Arrange

        // Create an instance of the PasswordVisibility class to be tested
        var passwordVisibilityHandler = new PasswordVisibility();

        // Toggle password visibility to make it initially visible
        passwordVisibilityHandler.ChangeVisibility();

        // Act

        // Call the ChangeVisibility method to toggle password visibility again
        passwordVisibilityHandler.ChangeVisibility();

        // Assert

        // Verify that the InputType property is set to InputType.Password, indicating that the input type is reverted to password when visibility is toggled off
        Assert.True(passwordVisibilityHandler.InputType == InputType.Password, "Input type should revert to password when visibility is toggled off.");
    }

    [Fact]
    public void ChangeVisibility_ShouldDisplayVisibilityOffIcon_WhenPasswordIsHidden()
    {
        // Arrange

        // Create an instance of the PasswordVisibility class to be tested
        var passwordVisibilityHandler = new PasswordVisibility();

        // Toggle password visibility to make it initially visible
        passwordVisibilityHandler.ChangeVisibility();

        // Act

        // Call the ChangeVisibility method to toggle password visibility again
        passwordVisibilityHandler.ChangeVisibility();

        // Assert

        // Verify that the Icon property is set to Icons.Material.Filled.VisibilityOff, indicating that the visibility off icon is displayed when the password is hidden
        Assert.True(passwordVisibilityHandler.Icon == Icons.Material.Filled.VisibilityOff, "Visibility off icon should be displayed when password is hidden.");
    }
}
