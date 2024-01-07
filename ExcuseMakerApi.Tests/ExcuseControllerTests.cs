using DTO;
using ExcuseMakerApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using Persistance.Interfaces;
using Services.Interfaces;
using Services.Services;

namespace ExcuseMakerApi.Tests;

[TestFixture]
public class ExcuseControllerTests
{
    [Test]
    public async Task GetExcuseById_ExistingId_ReturnsExcuse()
    {
        // Arrange
        var mockDatabase = new Mock<IExcuseDatabase>();
        var mockLogger = new Mock<ILogger<ExcuseService>>();
        var excuseService = new ExcuseService(mockDatabase.Object, mockLogger.Object);
        var existingId = 1;
        var expectedExcuse = new Excuse { Id = existingId };

        mockDatabase.Setup(db => db.GetExcuseById(existingId)).ReturnsAsync(expectedExcuse);

        // Act
        var result = await excuseService.GetExcuseById(existingId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(existingId));
    }

    [Test]
    public async Task GetAllExcuses_EmptyList_ReturnsEmptyList()
    {
        // Arrange
        var mockDatabase = new Mock<IExcuseDatabase>();
        var mockLogger = new Mock<ILogger<ExcuseService>>();
        var excuseService = new ExcuseService(mockDatabase.Object, mockLogger.Object);
        IEnumerable<Excuse> emptyList = new List<Excuse>();

        mockDatabase.Setup(db => db.GetAllExcuses()).ReturnsAsync(emptyList);

        // Act
        var result = await excuseService.GetAllExcuses();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task DeleteExcuseById_ExistingId_DeletedSuccessfully()
    {
        // Arrange
        var mockService = new Mock<IExcuseService>();
        var excuseController = new ExcuseController(mockService.Object);
        var existingId = 1;

        mockService.Setup(service => service.DeleteExcuse(existingId)).ReturnsAsync(true);

        // Act
        var result = await excuseController.DeleteExcuseById(existingId) as OkObjectResult;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.EqualTo($"deleted ({existingId})"));
    }

    [Test]
    public async Task GetRandomExcuse_ExistingCategory_ReturnsRandomExcuse()
    {
        // Arrange
        var mockService = new Mock<IExcuseService>();
        var excuseController = new ExcuseController(mockService.Object);
        var existingCategory = ExcuseCategory.Office;
        var randomExcuse = new Excuse();

        mockService.Setup(service => service.GetRandomExcuse(existingCategory)).ReturnsAsync(randomExcuse);

        // Act
        var result = await excuseController.GetRandomExcuse(existingCategory) as OkObjectResult;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.EqualTo(randomExcuse.Text));
    }
}
