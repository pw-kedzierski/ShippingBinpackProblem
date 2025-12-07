using BinPackingApp.Core.Models;
using BinPackingApp.Core.Services;
using Xunit;

namespace BinPackingApp.Tests;

public class BinPackServiceTests
{
    private readonly IBinPackService _binPackService;

    public BinPackServiceTests()
    {
        _binPackService = new BinPackService();
    }

    [Fact]
    public void CanPlaceShip_WhenShipIsWithinBounds_ReturnsTrue()
    {
        // Arrange
        var ship = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Test Ship",
            X = 0,
            Y = 0,
            IsRotated = false
        };
        var placedShips = new List<PlacedShip>();
        int anchorageWidth = 10;
        int anchorageHeight = 10;

        // Act
        var result = _binPackService.CanPlaceShip(ship, placedShips, anchorageWidth, anchorageHeight);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanPlaceShip_WhenShipExceedsWidth_ReturnsFalse()
    {
        // Arrange
        var ship = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Test Ship",
            X = 9, // Ship would extend to x=12, but anchorage width is 10
            Y = 0,
            IsRotated = false
        };
        var placedShips = new List<PlacedShip>();
        int anchorageWidth = 10;
        int anchorageHeight = 10;

        // Act
        var result = _binPackService.CanPlaceShip(ship, placedShips, anchorageWidth, anchorageHeight);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanPlaceShip_WhenShipExceedsHeight_ReturnsFalse()
    {
        // Arrange
        var ship = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Test Ship",
            X = 0,
            Y = 9, // Ship would extend to y=11, but anchorage height is 10
            IsRotated = false
        };
        var placedShips = new List<PlacedShip>();
        int anchorageWidth = 10;
        int anchorageHeight = 10;

        // Act
        var result = _binPackService.CanPlaceShip(ship, placedShips, anchorageWidth, anchorageHeight);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanPlaceShip_WhenShipHasNegativeX_ReturnsFalse()
    {
        // Arrange
        var ship = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Test Ship",
            X = -1,
            Y = 0,
            IsRotated = false
        };
        var placedShips = new List<PlacedShip>();
        int anchorageWidth = 10;
        int anchorageHeight = 10;

        // Act
        var result = _binPackService.CanPlaceShip(ship, placedShips, anchorageWidth, anchorageHeight);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanPlaceShip_WhenShipsOverlap_ReturnsFalse()
    {
        // Arrange
        var existingShip = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Existing Ship",
            X = 2,
            Y = 2,
            IsRotated = false
        };
        var newShip = new PlacedShip
        {
            Id = 2,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "New Ship",
            X = 3, // Overlaps with existing ship
            Y = 2,
            IsRotated = false
        };
        var placedShips = new List<PlacedShip> { existingShip };
        int anchorageWidth = 10;
        int anchorageHeight = 10;

        // Act
        var result = _binPackService.CanPlaceShip(newShip, placedShips, anchorageWidth, anchorageHeight);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanPlaceShip_WhenShipsDoNotOverlap_ReturnsTrue()
    {
        // Arrange
        var existingShip = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Existing Ship",
            X = 0,
            Y = 0,
            IsRotated = false
        };
        var newShip = new PlacedShip
        {
            Id = 2,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "New Ship",
            X = 4, // No overlap
            Y = 0,
            IsRotated = false
        };
        var placedShips = new List<PlacedShip> { existingShip };
        int anchorageWidth = 10;
        int anchorageHeight = 10;

        // Act
        var result = _binPackService.CanPlaceShip(newShip, placedShips, anchorageWidth, anchorageHeight);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanPlaceShip_WhenMovingSameShip_ReturnsTrue()
    {
        // Arrange
        var ship = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Test Ship",
            X = 0,
            Y = 0,
            IsRotated = false
        };
        var placedShips = new List<PlacedShip> { ship };
        int anchorageWidth = 10;
        int anchorageHeight = 10;

        // Act - Moving the same ship to a new position
        ship.X = 5;
        ship.Y = 5;
        var result = _binPackService.CanPlaceShip(ship, placedShips, anchorageWidth, anchorageHeight);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void PlaceShip_WhenNewShip_AddsToPlacedShips()
    {
        // Arrange
        var ship = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Test Ship",
            X = 0,
            Y = 0,
            IsRotated = false
        };
        var placedShips = new List<PlacedShip>();

        // Act
        _binPackService.PlaceShip(ship, placedShips);

        // Assert
        Assert.Single(placedShips);
        Assert.Equal(ship, placedShips[0]);
    }

    [Fact]
    public void PlaceShip_WhenExistingShip_UpdatesPosition()
    {
        // Arrange
        var ship = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Test Ship",
            X = 0,
            Y = 0,
            IsRotated = false
        };
        var placedShips = new List<PlacedShip> { ship };

        // Act
        ship.X = 5;
        ship.Y = 5;
        _binPackService.PlaceShip(ship, placedShips);

        // Assert
        Assert.Single(placedShips);
        Assert.Equal(5, placedShips[0].X);
        Assert.Equal(5, placedShips[0].Y);
    }

    [Fact]
    public void RemoveShip_WhenShipExists_RemovesFromPlacedShips()
    {
        // Arrange
        var ship = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Test Ship",
            X = 0,
            Y = 0,
            IsRotated = false
        };
        var placedShips = new List<PlacedShip> { ship };

        // Act
        _binPackService.RemoveShip(1, placedShips);

        // Assert
        Assert.Empty(placedShips);
    }

    [Fact]
    public void RemoveShip_WhenShipDoesNotExist_DoesNothing()
    {
        // Arrange
        var placedShips = new List<PlacedShip>();

        // Act
        _binPackService.RemoveShip(999, placedShips);

        // Assert
        Assert.Empty(placedShips);
    }

    [Fact]
    public void GetShipAt_WhenShipExists_ReturnsShip()
    {
        // Arrange
        var ship = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 3, Height = 2 },
            Designation = "Test Ship",
            X = 2,
            Y = 2,
            IsRotated = false
        };
        var placedShips = new List<PlacedShip> { ship };

        // Act
        var result = _binPackService.GetShipAt(3, 3, placedShips);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ship, result);
    }

    [Fact]
    public void GetShipAt_WhenNoShipExists_ReturnsNull()
    {
        // Arrange
        var placedShips = new List<PlacedShip>();

        // Act
        var result = _binPackService.GetShipAt(5, 5, placedShips);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void CanPlaceShip_WhenRotatedShipFits_ReturnsTrue()
    {
        // Arrange
        var ship = new PlacedShip
        {
            Id = 1,
            Dimensions = new ShipDimensions { Width = 5, Height = 2 }, // 5x2
            Designation = "Test Ship",
            X = 0,
            Y = 0,
            IsRotated = true // Becomes 2x5
        };
        var placedShips = new List<PlacedShip>();
        int anchorageWidth = 3; // Narrow anchorage
        int anchorageHeight = 10; // Tall anchorage

        // Act
        var result = _binPackService.CanPlaceShip(ship, placedShips, anchorageWidth, anchorageHeight);

        // Assert
        Assert.True(result);
        Assert.Equal(2, ship.ActualWidth);
        Assert.Equal(5, ship.ActualHeight);
    }
}

