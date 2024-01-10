using Xunit;
using HomeOrganizer.Models.User;

// Description:
// This unit test class is designed to test the behavior of the UserCredentials class.
// UserCredentials is responsible for managing a user's login credentials, including password hashing and verification.
// The test suite verifies that the constructor correctly initializes properties, the ResetPassword method updates credentials accurately,
// and the CheckPassword method correctly validates provided passwords.

public class UserCredentialsTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        // Setting up user credentials for the test with a predefined login and password.
        string login = "testUser";
        string password = "password123";

        // Act
        // Creating an instance of UserCredentials with the provided login and password.
        var userCredentials = new UserCredentials(login, password);

        // Assert
        // Checking if the UserCredentials instance is initialized with the correct login,
        // and non-null password hash and salt.
        Assert.Equal(login, userCredentials.Login);
        Assert.NotNull(userCredentials.PasswordHash);
        Assert.NotNull(userCredentials.PasswordSalt);
    }

    [Fact]
    public void ResetPassword_ShouldUpdatePasswordHashAndSalt()
    {
        // Arrange
        // Creating a UserCredentials instance with initial credentials.
        var userCredentials = new UserCredentials("testUser", "password123");
        string newPassword = "newPassword123";
        string originalHash = userCredentials.PasswordHash;
        string originalSalt = userCredentials.PasswordSalt;

        // Act
        // Updating the user's password to a new one.
        userCredentials.ResetPassword(newPassword);

        // Assert
        // Verifying that the password hash and salt have been updated to new values.
        Assert.NotEqual(originalHash, userCredentials.PasswordHash);
        Assert.NotEqual(originalSalt, userCredentials.PasswordSalt);
    }

    [Fact]
    public void CheckPassword_WithValidPassword_ShouldReturnTrue()
    {
        // Arrange
        // Creating a UserCredentials instance with a specific password.
        string password = "password123";
        var userCredentials = new UserCredentials("testUser", password);

        // Act
        // Checking the password against the stored hash and salt.
        bool isPasswordValid = userCredentials.CheckPassword(password);

        // Assert
        // Expecting the password check to succeed.
        Assert.True(isPasswordValid);
    }

    [Fact]
    public void CheckPassword_WithInvalidPassword_ShouldReturnFalse()
    {
        // Arrange
        // Creating a UserCredentials instance with a specific password.
        var userCredentials = new UserCredentials("testUser", "password123");

        // Act
        // Checking a wrong password against the stored hash and salt.
        bool isPasswordValid = userCredentials.CheckPassword("wrongPassword");

        // Assert
        // Expecting the password check to fail.
        Assert.False(isPasswordValid);
    }
}
