using DTO;
using ExcuseMakerApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using Polly.Timeout;
using Services.Interfaces;

namespace ExcuseMakerApi.Tests;

[TestFixture]
public class ExcuseControllerTests
{
    private Mock<IExcuseService> _excuseServiceMock;
    private ExcuseController _excuseController;

    [SetUp]
    public void Setup()
    {
        _excuseServiceMock = new Mock<IExcuseService>();
        _excuseController = new ExcuseController(_excuseServiceMock.Object);
    }

    [Test]
    public async Task GetRandomExcuse_WhenServiceThrowsHttpRequestException_ShouldReturnInternalServerError()
    {
        // Arrange
        _excuseServiceMock.Setup(x => x.GetRandomExcuse(It.IsAny<ExcuseCategory>()))
            .ThrowsAsync(new HttpRequestException("Simulated HTTP error"));

        // Act
        var result = await _excuseController.GetRandomExcuse(ExcuseCategory.Friends);

        // Assert
        var statusCodeResult = result as StatusCodeResult;
        Assert.IsNotNull(statusCodeResult);
        Assert.AreEqual(500, statusCodeResult.StatusCode);
    }

    [Test]
    public async Task GetRandomExcuse_WhenServiceTimesOut_ShouldReturnInternalServerError()
    {
        // Arrange
        _excuseServiceMock.Setup(x => x.GetRandomExcuse(It.IsAny<ExcuseCategory>()))
            .ThrowsAsync(new TimeoutRejectedException("Timeout occurred"));

        // Act
        var result = await _excuseController.GetRandomExcuse(ExcuseCategory.Office);

        // Assert
        var statusCodeResult = result as StatusCodeResult;
        Assert.IsNotNull(statusCodeResult);
        Assert.AreEqual(500, statusCodeResult.StatusCode);
    }
    
    [Test]
    public async Task GetRandomExcuse_WhenServiceReturnsNull_ShouldReturnNotFound()
    {
        // Arrange
        _excuseServiceMock.Setup(x => x.GetRandomExcuse(It.IsAny<ExcuseCategory>()))
            .ReturnsAsync((Excuse)null);

        // Act
        var result = await _excuseController.GetRandomExcuse(ExcuseCategory.Partner);

        // Assert
        var statusCodeResult = result as StatusCodeResult;
        Assert.IsNotNull(statusCodeResult);
        Assert.AreEqual(404, statusCodeResult.StatusCode);
    }
}
