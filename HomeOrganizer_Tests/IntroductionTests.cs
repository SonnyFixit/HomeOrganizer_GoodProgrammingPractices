using Xunit;
using HomeOrganizer.Models.Features;
using HomeOrganizer.Models.Bases;

// Description:
// This unit test class is designed to test the behavior of the Introduction class, which is a subclass of FeatureBase.
// The Introduction class represents a specific feature with predefined FeatureData and TileData.
// The tests verify the correct initialization of these properties and the functionality of the Create method,
// ensuring that the Introduction class properly represents its intended feature and behaves as expected.

public class IntroductionTests
{
    [Fact]
    public void FeatureData_ShouldBeCorrectlyInitialized()
    {
        // Arrange
        // Creating an instance of Introduction class to test its initial state.
        var introduction = new Introduction();

        // Act
        // No action is required here as we are testing the initial state.

        // Assert
        // Verifying that the properties of FeatureData are correctly initialized.
        Assert.Equal("Introduction", introduction.FeatureData.Name);
        Assert.Equal("First user's feature tile to play with and test it", introduction.FeatureData.Description);
        Assert.False(introduction.FeatureData.IsReusable);
        Assert.True(introduction.FeatureData.IsUsed);
        Assert.Equal("Introduction", introduction.FeatureData.PageName);
    }

    [Fact]
    public void TileData_ShouldBeCorrectlyInitialized()
    {
        // Arrange
        // Creating an instance of Introduction class to test its initial state.
        var introduction = new Introduction();

        // Act
        // No action is required here as we are testing the initial state.

        // Assert
        // Verifying that the properties of TileData are correctly initialized.
        Assert.Equal("Your first feature", introduction.TileData.UserGivenName);
        Assert.Equal("Check it! It should introduce you to our application :)", introduction.TileData.UserGivenDescription);
        Assert.Equal(0, introduction.TileData.Position);
        Assert.Equal(0, introduction.TileData.ColorIndex);
    }

    [Fact]
    public void Create_ShouldReturnNewInstanceOfIntroduction()
    {
        // Arrange
        // Creating an instance of Introduction class to test the Create method.
        var introduction = new Introduction();

        // Act
        // Calling the Create method to get a new instance of Introduction.
        var newFeature = introduction.Create();

        // Assert
        // Verifying that the Create method returns a new instance of Introduction.
        Assert.IsType<Introduction>(newFeature);
    }
}
