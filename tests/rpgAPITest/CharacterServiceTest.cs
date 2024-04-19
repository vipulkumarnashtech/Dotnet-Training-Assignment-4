using rpgAPI.Model;
using rpgAPI.Service;
using Xunit;

namespace rpgAPITest;

public class CharacterServiceTest
{
    private readonly CharacterService _characterService;
    public CharacterServiceTest()
    {
        _characterService = new CharacterService();
    }
    [Fact]
    public void UpdateCharacterExistingCharacterSuccess()
    {

        // Arrange
        var existingCharacter = new Character
        {
            Id = 3,
            Name = "Aragorn",
            HitPoint = 50,
            Strength = 10,
            Defense = 5,
            Intelligence = 8,
            CharacterClass = RPGClass.Mage
        };
        _characterService.AddCharacter(existingCharacter);

        var updatedCharacter = new Character
        {
            Id = 3,
            Name = "Aragorn II",
            HitPoint = 100,
            Strength = 20,
            Defense = 10,
            Intelligence = 15,
            CharacterClass = RPGClass.Knight
        };

        // Act
        var serviceResponse = _characterService.UpdateCharacter(updatedCharacter);

        // Assert
        Assert.True(serviceResponse.Success);
        var result = serviceResponse.Data.FirstOrDefault(c => c.Id == 3);
        Assert.Equal("Aragorn II", result.Name);
    }

    [Fact]
    public void UpdateCharacterNonExistingCharacterFailure()
    {
        // Arrange
        var updatedCharacter = new Character
        {
            Id = 999, // Non-existing ID
            Name = "Sauron",
            HitPoint = 100,
            Strength = 20,
            Defense = 10,
            Intelligence = 15,
            CharacterClass = RPGClass.Knight
        };

        // Act
        var serviceResponse = _characterService.UpdateCharacter(updatedCharacter);

        // Assert
        Assert.False(serviceResponse.Success);
        Assert.Equal("Id Doesn't Exist", serviceResponse.Message);
    }
    [Fact]
    public void DeleteCharacter_RemovesCharacter_IfExists()
    {
        // Arrange
        var characterService = new CharacterService();

        // Act
        var result = characterService.DeleteCharacter(1);

        // Assert
        Assert.True(result.Success);
        Assert.DoesNotContain(result.Data, c => c.Id == 1);
    }

    [Fact]
    public void DeleteCharacter_ReturnsError_IfIdDoesNotExist()
    {
        // Arrange
        var characterService = new CharacterService();

        // Act
        var result = characterService.DeleteCharacter(4);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Id Doesn't Exist", result.Message);
    }
}