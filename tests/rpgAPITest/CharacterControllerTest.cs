using Microsoft.AspNetCore.Mvc;
using Moq;
using rpgAPI.Controller;
using rpgAPI.Model;
using rpgAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace rpgAPITest
{
    public class CharacterControllerTests
    {
        [Fact]
        public void PutCharacter_ReturnsOk_WithUpdatedCharacter()
        {
            // Arrange
            var mockCharacterService = new Mock<ICharacterService>();
            var controller = new CharacterController(mockCharacterService.Object);
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
            var serviceResponse = new ServiceResponse<List<Character>>
            {
                Success = true,
                Data = new List<Character> { updatedCharacter }
            };
            mockCharacterService.Setup(service => service.UpdateCharacter(updatedCharacter)).Returns(serviceResponse);

            // Act
            var result = controller.PutCharacter(updatedCharacter);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedServiceResponse = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
            Assert.True(returnedServiceResponse.Success);
            Assert.Equal("Aragorn II", returnedServiceResponse.Data[0].Name);
        }
        [Fact]
        public void DeleteCharacter_ReturnsOk_WithDeletedCharacter()
        {
            // Arrange
            var mockCharacterService = new Mock<ICharacterService>();
            var controller = new CharacterController(mockCharacterService.Object);
            var idToDelete = 1;
            var serviceResponse = new ServiceResponse<List<Character>>
            {
                Success = true,
                Data = new List<Character>() // Assuming this list contains the characters after deletion
            };
            mockCharacterService.Setup(service => service.DeleteCharacter(idToDelete)).Returns(serviceResponse);

            // Act
            var result = controller.DeleteCharacter(idToDelete);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedServiceResponse = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
            Assert.True(returnedServiceResponse.Success);
        }
    }
