using Moq;
using Xunit;
using HomeOrganizer.Models.User;
using HomeOrganizer.Services;
using System.Threading.Tasks;

// Description:
// This unit test class is designed to test the behavior of the UserService class.
// The primary focus of this test is to verify that the UpdateUser method of the UserService
// correctly updates the LoggedUser property and triggers two events: OnChange and AsyncOnChange.
// OnChange is used for notifying observers of changes to the user data, while AsyncOnChange is
// used for asynchronous notifications.

public class UserServiceTests
{
    [Fact]
    public async Task UpdateUser_ShouldUpdateLoggedUserAndInvokeOnChange()
    {
        // Arrange

        // Create an instance of the UserService class to be tested
        var userService = new UserService();

        // Create a new user object to simulate the updated user data
        var newUser = new UserData();

        // Initialize variables to track whether the event handlers are called
        var onChangeCalled = false; // Tracks the regular event
        var asyncOnChangeCalled = false; // Tracks the async event

        // Subscribe to the OnChange event with a lambda expression that sets onChangeCalled to true
        userService.OnChange += () => onChangeCalled = true;

        // Subscribe to the AsyncOnChange event with a lambda expression that sets asyncOnChangeCalled to true
        userService.AsyncOnChange += () => { asyncOnChangeCalled = true; return Task.CompletedTask; };

        // Act

        // Call the UpdateUser method with the new user data, which should trigger the events
        await userService.UpdateUser(newUser);

        // Assert

        // Check if the LoggedUser property of the userService has been updated to the new user object
        Assert.Equal(newUser, userService.LoggedUser);

        // Check if the regular event (OnChange) was invoked, indicating that the user data has changed
        Assert.True(onChangeCalled);

        // Check if the async event (AsyncOnChange) was invoked, indicating an asynchronous change in user data
        Assert.True(asyncOnChangeCalled);
    }
}