using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ProgiAPI.Controllers;
using ProgiAPI.DbLogic;
using ProgiAPI.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MainControllerTests
{
    private readonly Mock<IConnections> _mockConnections;
    private readonly MainController _controller;

    public MainControllerTests()
    {
        _mockConnections = new Mock<IConnections>();
        _controller = new MainController(null, _mockConnections.Object);
    }

    [Fact]
    public async Task GetAssociationFees_ReturnsOkResult_WithAssociationFees()
    {
        var associationFees = new List<AssociationFees> { new AssociationFees() };
        _mockConnections.Setup(repo => repo.GetAssociationFees()).ReturnsAsync(associationFees);

        var result = await _controller.GetAssociationFees();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(associationFees, okResult.Value);
    }

    [Fact]
    public async Task GetBuyerSellerFees_ReturnsOkResult_WithBuyerSellerFees()
    {
        var carType = 1;
        var buyerSellerFees = new List<BuyerSellerFees> { new BuyerSellerFees() };
        _mockConnections.Setup(repo => repo.GetBuyerSellerFees(carType)).ReturnsAsync(buyerSellerFees);

        var result = await _controller.GetBuyerSellerFees(carType);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(buyerSellerFees, okResult.Value);
    }

    [Fact]
    public async Task GetBuyerSellerFees_ReturnsNotFound_WhenNoFeesFound()
    {
        var carType = 1;
        _mockConnections.Setup(repo => repo.GetBuyerSellerFees(carType)).ReturnsAsync(new List<BuyerSellerFees>());

        var result = await _controller.GetBuyerSellerFees(carType);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetCarTypes_ReturnsOkResult_WithCarTypes()
    {
        var carTypes = new List<CarTypes> { new CarTypes() };
        _mockConnections.Setup(repo => repo.GetCarTypes()).ReturnsAsync(carTypes);

        var result = await _controller.GetCarTypes();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(carTypes, okResult.Value);
    }

    [Fact]
    public async Task GetStorageFees_ReturnsOkResult_WithStorageFees()
    {
        var storageFee = new StorageFee();
        _mockConnections.Setup(repo => repo.GetStorageFees()).ReturnsAsync(storageFee);

        var result = await _controller.GetStorageFees();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(storageFee, okResult.Value);
    }

    [Fact]
    public async Task GetStorageFees_ReturnsNotFound_WhenStorageFeesAreNull()
    {
        _mockConnections.Setup(repo => repo.GetStorageFees()).ReturnsAsync((StorageFee)null);

        var result = await _controller.GetStorageFees();

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateStorageFees_ReturnsOkResult_WhenUpdateIsSuccessful()
    {
        var newStorageFee = 100;
        _mockConnections.Setup(repo => repo.UpdateStorageFees(newStorageFee)).ReturnsAsync(true);

        var result = await _controller.UpdateStorageFees(newStorageFee);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task UpdateStorageFees_ReturnsInternalServerError_WhenUpdateFails()
    {
        var newStorageFee = 100;
        _mockConnections.Setup(repo => repo.UpdateStorageFees(newStorageFee)).ReturnsAsync(false);

        var result = await _controller.UpdateStorageFees(newStorageFee);

        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
    }
}